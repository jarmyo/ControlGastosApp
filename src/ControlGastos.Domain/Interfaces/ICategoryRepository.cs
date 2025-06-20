using ControlGastos.Domain.Entities;

namespace ControlGastos.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category cat);
        Task UpdateAsync(Category cat);
        Task DeleteAsync(int id);
    }
    }