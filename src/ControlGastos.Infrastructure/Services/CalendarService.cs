using ControlGastos.Domain.Interfaces;

namespace ControlGastos.Infrastructure.Services
{
    public class CalendarService : ICalendarService
    {
        public DateTime AdjustPaymentDate(int year, int month, int day)
        {
            var date = new DateTime(year, month, Math.Min(day, DateTime.DaysInMonth(year, month)));
            if (date.DayOfWeek == DayOfWeek.Saturday)
                return date.AddDays(-1);
            if (date.DayOfWeek == DayOfWeek.Sunday)
                return date.AddDays(-2);
            return date;
        }
    }
}

