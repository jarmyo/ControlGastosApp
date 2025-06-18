using ControlGastos.Domain.Entities;
using ControlGastos.Infrastructure.Data;
using ControlGastos.Infrastructure.Repositories;
using ControlGastos.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.Infrastructure.Tests
{
    public class CalculationServiceTests
    {
        private async Task<CalculationService> CreateServiceWithDataAsync()
        {
            var options = new DbContextOptionsBuilder<ControlGastosDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var ctx = new ControlGastosDbContext(options);

            // Crear gastos
            ctx.RecurringExpenses.Add(new RecurringExpense
            {
                Id = 1,
                Name = "Fijo",
                Type = ExpenseType.Fixed,
                FixedAmount = 100,
                DayOfPayment = 1
            });
            ctx.RecurringExpenses.Add(new RecurringExpense
            {
                Id = 2,
                Name = "Aprox",
                Type = ExpenseType.Approximate,
                ApproximateAmount = 50,
                DayOfPayment = 1
            });

            // Crear pagos (sólo el fijo)
            ctx.Payments.Add(new Payment
            {
                RecurringExpenseId = 1,
                Amount = 100,
                PaymentDate = new DateTime(2025, 6, 1)
            });

            await ctx.SaveChangesAsync();

            var expRepo = new EfRecurringExpenseRepository(ctx);
            var payRepo = new EfPaymentRepository(ctx);
            return new CalculationService(expRepo, payRepo);
        }

        [Fact]
        public async Task GetTotalPaidAsync_ReturnsSumOfPaid()
        {
            var svc = await CreateServiceWithDataAsync();
            var totalPaid = await svc.GetTotalPaidAsync(2025, 6);
            Assert.Equal(100m, totalPaid);
        }

        [Fact]
        public async Task GetTotalPendingAsync_ReturnsFixedAndApproxUnpaid()
        {
            var svc = await CreateServiceWithDataAsync();
            // Sólo el 2° gasto (aprox 50) queda pendiente
            var totalPending = await svc.GetTotalPendingAsync(2025, 6);
            Assert.Equal(50m, totalPending);
        }
    }
}
