using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;
using ControlGastos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.Infrastructure.Repositories
{
    public class EfRecurringIncomeRepository : IRecurringIncomeRepository
    {
        private readonly ControlGastosDbContext _ctx;
        public EfRecurringIncomeRepository(ControlGastosDbContext ctx) => _ctx = ctx;

        public Task<IEnumerable<RecurringIncome>> GetAllAsync() =>
            _ctx.RecurringIncomes.ToListAsync().ContinueWith(t => (IEnumerable<RecurringIncome>)t.Result);

        public Task<RecurringIncome?> GetByIdAsync(int id) =>
            _ctx.RecurringIncomes.FindAsync(id).AsTask();

        public async Task AddAsync(RecurringIncome income)
        {
            _ctx.RecurringIncomes.Add(income);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(RecurringIncome income)
        {
            _ctx.RecurringIncomes.Update(income);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var i = await _ctx.RecurringIncomes.FindAsync(id);
            if (i != null)
            {
                _ctx.RecurringIncomes.Remove(i);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}

