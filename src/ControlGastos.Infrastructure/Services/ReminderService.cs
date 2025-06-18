using ControlGastos.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ControlGastos.Infrastructure.Services
{
    public class ReminderService : BackgroundService
    {
        private readonly ILogger<ReminderService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly int _hour;
        private readonly int _minute;

        public ReminderService(
            ILogger<ReminderService> logger,
            IServiceScopeFactory scopeFactory,
            IConfiguration config)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _hour = int.Parse(config["Reminder:Hour"]);
            _minute = int.Parse(config["Reminder:Minute"]);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Calcula cuánto tiempo falta hasta la siguiente hora programada
                var now = DateTime.Now;
                var nextRun = new DateTime(now.Year, now.Month, now.Day, _hour, _minute, 0);
                if (nextRun <= now)
                    nextRun = nextRun.AddDays(1);

                var delay = nextRun - now;
                _logger.LogInformation("ReminderService durmiendo hasta {NextRun}", nextRun);
                await Task.Delay(delay, stoppingToken);

                // Cuando llega la hora:
                try
                {
                    await SendRemindersAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al enviar recordatorios");
                }
            }
        }

        private async Task SendRemindersAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var expRepo = scope.ServiceProvider.GetRequiredService<IRecurringExpenseRepository>();
            var payRepo = scope.ServiceProvider.GetRequiredService<IPaymentRepository>();
            var calSvc = scope.ServiceProvider.GetRequiredService<ICalendarService>();

            var today = DateTime.Today;
            var year = today.Year;
            var month = today.Month;

            var all = await expRepo.GetAllAsync();
            var pagos = await payRepo.GetByMonthAsync(year, month);
            var pagadasIds = pagos.Select(p => p.RecurringExpenseId).ToHashSet();

            var pendientes = all
                .Where(e => !pagadasIds.Contains(e.Id))
                .Select(e => new
                {
                    e.Name,
                    Due = calSvc.AdjustPaymentDate(year, month, e.DayOfPayment)
                })
                .Where(x => x.Due >= today)
                .ToList();

            if (pendientes.Count > 0)
            {
                _logger.LogInformation("=== RECORDATORIO DE PAGOS PENDIENTES ===");
                foreach (var p in pendientes)
                    _logger.LogInformation("Pendiente: {Name} - Fecha: {Due:d}", p.Name, p.Due);
            }
            else
            {
                _logger.LogInformation("No hay pagos pendientes para recordar hoy.");
            }
        }
    }
}

