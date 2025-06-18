using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;
using ControlGastos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.Infrastructure.Repositories
{
    public class EfRecurringExpenseRepository : IRecurringExpenseRepository
    {
        private readonly ControlGastosDbContext _ctx;
        public EfRecurringExpenseRepository(ControlGastosDbContext ctx) => _ctx = ctx;

        public Task<IEnumerable<RecurringExpense>> GetAllAsync() =>
            _ctx.RecurringExpenses.ToListAsync().ContinueWith(t => (IEnumerable<RecurringExpense>)t.Result);

        public Task<RecurringExpense?> GetByIdAsync(int id) =>
            _ctx.RecurringExpenses.FindAsync(id).AsTask();

        public async Task AddAsync(RecurringExpense expense)
        {
            _ctx.RecurringExpenses.Add(expense);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(RecurringExpense expense)
        {
            _ctx.RecurringExpenses.Update(expense);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _ctx.RecurringExpenses.FindAsync(id);
            if (e != null)
            {
                _ctx.RecurringExpenses.Remove(e);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}