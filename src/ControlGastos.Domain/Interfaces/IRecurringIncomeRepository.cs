using ControlGastos.Domain.Entities;

namespace ControlGastos.Domain.Interfaces
{
    public interface IRecurringIncomeRepository
    {
        Task<IEnumerable<RecurringIncome>> GetAllAsync();
        Task<RecurringIncome?> GetByIdAsync(int id);
        Task AddAsync(RecurringIncome income);
        Task UpdateAsync(RecurringIncome income);
        Task DeleteAsync(int id);
    }
}