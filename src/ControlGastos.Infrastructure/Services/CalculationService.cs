using ControlGastos.Domain.Interfaces;

namespace ControlGastos.Infrastructure.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly IRecurringExpenseRepository _expRepo;
        private readonly IPaymentRepository _payRepo;

        public CalculationService(
            IRecurringExpenseRepository expRepo,
            IPaymentRepository payRepo)
        {
            _expRepo = expRepo;
            _payRepo = payRepo;
        }

        public async Task<decimal> GetTotalPaidAsync(int year, int month)
        {
            var pagos = await _payRepo.GetByMonthAsync(year, month);
            decimal sum = 0;
            foreach (var p in pagos) sum += p.Amount;
            return sum;
        }

        public async Task<decimal> GetTotalPendingAsync(int year, int month)
        {
            var gastos = await _expRepo.GetAllAsync();
            var pagos = await _payRepo.GetByMonthAsync(year, month);
            decimal totalFixed = 0, totalApprox = 0;
            foreach (var g in gastos)
            {
                bool paid = false;
                foreach (var p in pagos)
                    if (p.RecurringExpenseId == g.Id) { paid = true; break; }

                if (!paid)
                {
                    if (g.Type == Domain.Entities.ExpenseType.Fixed)
                        totalFixed += g.FixedAmount ?? 0;
                    else
                        totalApprox += g.ApproximateAmount ?? 0;
                }
            }
            return totalFixed + totalApprox;
        }
    }
}

