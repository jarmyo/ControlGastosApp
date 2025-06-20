using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;
using ControlGastos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.Infrastructure.Repositories
{
    public class ExpenseInstallmentRepository : IExpenseInstallmentRepository
    {
        readonly ControlGastosDbContext _ctx;
        public ExpenseInstallmentRepository(ControlGastosDbContext ctx) => _ctx = ctx;
        public Task<IEnumerable<ExpenseInstallment>> GetByExpenseAsync(int expenseId) =>
            _ctx.ExpenseInstallments
                .Where(x => x.RecurringExpenseId == expenseId)
                .ToListAsync().ContinueWith(t => (IEnumerable<ExpenseInstallment>)t.Result);
        public async Task AddAsync(ExpenseInstallment inst) { _ctx.ExpenseInstallments.Add(inst); await _ctx.SaveChangesAsync(); }
        public async Task DeleteByExpenseAsync(int expenseId)
        {
            var list = _ctx.ExpenseInstallments.Where(x => x.RecurringExpenseId == expenseId);
            _ctx.ExpenseInstallments.RemoveRange(list);
            await _ctx.SaveChangesAsync();
        }
    }
}
