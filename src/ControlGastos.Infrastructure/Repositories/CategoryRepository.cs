using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;
using ControlGastos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly ControlGastosDbContext _ctx;
        public CategoryRepository(ControlGastosDbContext ctx) => _ctx = ctx;
        public Task<IEnumerable<Category>> GetAllAsync() =>
            _ctx.Categories.ToListAsync().ContinueWith(t => (IEnumerable<Category>)t.Result);
        public Task<Category?> GetByIdAsync(int id) => _ctx.Categories.FindAsync(id).AsTask();
        public async Task AddAsync(Category cat) { _ctx.Categories.Add(cat); await _ctx.SaveChangesAsync(); }
        public async Task UpdateAsync(Category cat) { _ctx.Categories.Update(cat); await _ctx.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var c = await _ctx.Categories.FindAsync(id);
            if (c != null) { _ctx.Categories.Remove(c); await _ctx.SaveChangesAsync(); }
        }
    }
}
