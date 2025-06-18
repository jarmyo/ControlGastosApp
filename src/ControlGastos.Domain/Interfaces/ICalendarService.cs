namespace ControlGastos.Domain.Interfaces
{
    public interface ICalendarService
    {
        /// <summary>
        /// Dado un día del mes (1–31) y un mes/año, devuelve la fecha ajustada
        /// al viernes anterior si cae en fin de semana.
        /// </summary>
        DateTime AdjustPaymentDate(int year, int month, int day);
    }
}