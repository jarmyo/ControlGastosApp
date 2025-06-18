using ControlGastos.Domain.Interfaces;
using ControlGastos.Infrastructure.Data;
using ControlGastos.Infrastructure.Repositories;
using ControlGastos.Infrastructure.Services;
using ControlGastos.Mobile.ViewModels;
using ControlGastos.Mobile.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ControlGastos.Mobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        // Ruta de la DB local

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "controlgastos.db");
        builder.Services.AddDbContext<ControlGastosDbContext>(opts =>
            opts.UseSqlite(
              $"Data Source={dbPath}",
              sql => sql.MigrationsAssembly("ControlGastos.Infrastructure")
            ));

        // Repositorios y servicios
        builder.Services.AddScoped<IRecurringExpenseRepository, EfRecurringExpenseRepository>();
        builder.Services.AddScoped<IRecurringIncomeRepository, EfRecurringIncomeRepository>();
        builder.Services.AddScoped<IPaymentRepository, EfPaymentRepository>();
        builder.Services.AddScoped<ICalendarService, CalendarService>();
        builder.Services.AddScoped<ICalculationService, CalculationService>();
        

        // ViewModels y páginas
        builder.Services.AddTransient<DashboardViewModel>();
        builder.Services.AddTransient<DashboardPage>();

        builder.Services.AddTransient<ExpensesViewModel>();
        builder.Services.AddTransient<ExpensesPage>();

        builder.Services.AddTransient<PaymentsViewModel>();
        builder.Services.AddTransient<PaymentsPage>();

        builder.Services.AddTransient<IncomesViewModel>();
        builder.Services.AddTransient<IncomesPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var mauiApp = builder.Build();

        // Aplica migraciones para crear todas las tablas si no existen
        using (var scope = mauiApp.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ControlGastosDbContext>();
            db.Database.Migrate();
        }

        return mauiApp;
    }
}

