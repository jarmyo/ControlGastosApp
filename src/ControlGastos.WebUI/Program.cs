using ControlGastos.Domain.Interfaces;
using ControlGastos.Infrastructure.Data;
using ControlGastos.Infrastructure.Repositories;
using ControlGastos.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.WebUI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1) Registrar DbContext
            var connStr = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "Data Source=controlgastos.db";

            builder.Services
                .AddDbContext<ControlGastosDbContext>(opts => opts
                .UseSqlite(connStr, sql => sql
                .MigrationsAssembly("ControlGastos.Infrastructure")));

            builder.Services.AddScoped<IRecurringExpenseRepository, EfRecurringExpenseRepository>();
            builder.Services.AddScoped<IPaymentRepository, EfPaymentRepository>();
            builder.Services.AddScoped<IRecurringIncomeRepository, EfRecurringIncomeRepository>();
            builder.Services.AddScoped<ICalendarService, CalendarService>();
            builder.Services.AddScoped<ICalculationService, CalculationService>();
            builder.Services.AddScoped<IRecurringIncomeRepository, EfRecurringIncomeRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IExpenseInstallmentRepository, ExpenseInstallmentRepository>();
            builder.Services.AddHostedService<ReminderService>();

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services
                .AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddEntityFrameworkStores<ControlGastosDbContext>();

            // proteger todas las p�ginas
            builder.Services
                .AddAuthorizationBuilder()
                .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build());

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ControlGastosDbContext>();
                db.Database.Migrate();
            }

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/Dashboard/Index");
                return Task.CompletedTask;
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}