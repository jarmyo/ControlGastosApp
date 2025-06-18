namespace ControlGastos.Domain.Interfaces
{
    public interface ICalculationService
    {
        /// <summary>
        /// Suma de todos los pagos registrados en un mes.
        /// </summary>
        Task<decimal> GetTotalPaidAsync(int year, int month);

        /// <summary>
        /// Suma de montos fijos + aproximados pendientes en un mes.
        /// </summary>
        Task<decimal> GetTotalPendingAsync(int year, int month);
    }
}