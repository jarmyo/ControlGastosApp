using ControlGastos.Domain.Entities;

namespace ControlGastos.Domain.Interfaces
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetByMonthAsync(int year, int month);
        Task AddAsync(Payment payment);
    }
}