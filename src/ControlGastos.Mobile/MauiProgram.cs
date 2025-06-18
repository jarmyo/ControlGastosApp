using ControlGastos.Domain.Interfaces;
using ControlGastos.Infrastructure.Data;
using ControlGastos.Infrastructure.Repositories;
using ControlGastos.Infrastructure.Services;
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
          opts.UseSqlite($"Data Source={dbPath}"));

        // Repositorios y servicios
        builder.Services.AddScoped<IRecurringExpenseRepository, EfRecurringExpenseRepository>();
        builder.Services.AddScoped<IPaymentRepository, EfPaymentRepository>();
        builder.Services.AddScoped<ICalendarService, CalendarService>();
        builder.Services.AddScoped<ICalculationService, CalculationService>();

        // ViewModels y páginas
        //builder.Services.AddTransient<DashboardViewModel>();
        //builder.Services.AddTransient<DashboardPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
