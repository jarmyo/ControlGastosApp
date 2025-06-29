using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlGastos.WebUI.Pages.Payments
{
    public class IndexModel(
        IRecurringExpenseRepository expRepo,
        IPaymentRepository payRepo,
        ICalendarService calSvc) : PageModel
    {
        private readonly IRecurringExpenseRepository _expRepo = expRepo;
        private readonly IPaymentRepository _payRepo = payRepo;
        private readonly ICalendarService _calSvc = calSvc;

        public IEnumerable<PendingDto> Pending { get; private set; }

        public async Task OnGetAsync()
        {
            var year = DateTime.Today.Year;
            var month = DateTime.Today.Month;
            var all = await _expRepo.GetAllAsync();
            var pagos = await _payRepo.GetByMonthAsync(year, month);
            var pagadas = pagos.Select(p => p.RecurringExpenseId).ToHashSet();

            Pending = all
                .Where(e => !pagadas.Contains(e.Id))
                .Select(e => new PendingDto(e.Id, e.Name, e.Type,
                    e.FixedAmount ?? 0, e.ApproximateAmount ?? 0, _calSvc.AdjustPaymentDate(year, month, e.DayOfPayment)));
        }

        public record PendingDto(int Id, string Name, ExpenseType Type, decimal Fixed, decimal Approx, DateTime DueDate);
    }
}
