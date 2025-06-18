using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;
using ControlGastos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.Infrastructure.Repositories
{
    public class EfPaymentRepository : IPaymentRepository
    {
        private readonly ControlGastosDbContext _ctx;

        public EfPaymentRepository()
        {
        }

        public EfPaymentRepository(ControlGastosDbContext ctx) => _ctx = ctx;

        public Task AddAsync(Payment payment)
        {
            _ctx.Payments.Add(payment);
            return _ctx.SaveChangesAsync();
        }

        public Task<IEnumerable<Payment>> GetByMonthAsync(int year, int month)
        {
            var list = _ctx.Payments
                .Where(p => p.PaymentDate.Year == year && p.PaymentDate.Month == month)
                .ToListAsync();
            return list.ContinueWith(t => (IEnumerable<Payment>)t.Result);
        }
    }
}
