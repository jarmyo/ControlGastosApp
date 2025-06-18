using ControlGastos.Domain.Entities;

namespace ControlGastos.Domain.Interfaces
{
    public interface IRecurringExpenseRepository
    {
        Task<IEnumerable<RecurringExpense>> GetAllAsync();
        Task<RecurringExpense?> GetByIdAsync(int id);
        Task AddAsync(RecurringExpense expense);
        Task UpdateAsync(RecurringExpense expense);
        Task DeleteAsync(int id);
    }
}