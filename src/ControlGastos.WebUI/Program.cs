using ControlGastos.Domain.Interfaces;
using ControlGastos.Infrastructure.Data;
using ControlGastos.Infrastructure.Repositories;
using ControlGastos.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1) Registrar DbContext
var connStr = builder.Configuration.GetConnectionString("DefaultConnection")
           ?? "Data Source=controlgastos.db";

builder.Services.AddDbContext<ControlGastosDbContext>(opts =>
    opts.UseSqlite(connStr));

builder.Services.AddScoped<IRecurringExpenseRepository, EfRecurringExpenseRepository>();
builder.Services.AddScoped<IPaymentRepository, EfPaymentRepository>();
builder.Services.AddScoped<IRecurringIncomeRepository, EfRecurringIncomeRepository>();
builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<ICalculationService, CalculationService>();
builder.Services.AddScoped<IRecurringIncomeRepository, EfRecurringIncomeRepository>();
builder.Services.AddHostedService<ReminderService>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

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
