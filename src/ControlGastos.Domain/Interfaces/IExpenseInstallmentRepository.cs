using ControlGastos.Domain.Entities;

namespace ControlGastos.Domain.Interfaces
{
    public interface IExpenseInstallmentRepository
    {
        Task<IEnumerable<ExpenseInstallment>> GetByExpenseAsync(int expenseId);
        Task AddAsync(ExpenseInstallment inst);
        Task DeleteByExpenseAsync(int expenseId);
    }
    }