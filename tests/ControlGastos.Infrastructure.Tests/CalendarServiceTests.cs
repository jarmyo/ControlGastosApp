using ControlGastos.Infrastructure.Services;
using System;
using Xunit;

namespace ControlGastos.Infrastructure.Tests
{
    public class CalendarServiceTests
    {
        private readonly CalendarService _svc = new();

        [Theory]
        [InlineData(2025, 6, 14, 2025, 6, 13)] // sábado → viernes
        [InlineData(2025, 6, 15, 2025, 6, 13)] // domingo → viernes
        [InlineData(2025, 6, 16, 2025, 6, 16)] // lunes → lunes
        public void AdjustPaymentDate_WeekendsMovedToFriday(
            int year, int month, int day,
            int expYear, int expMonth, int expDay)
        {
            var result = _svc.AdjustPaymentDate(year, month, day);
            Assert.Equal(new DateTime(expYear, expMonth, expDay), result);
        }

        [Fact]
        public void AdjustPaymentDate_MaxDayClampsToMonth()
        {
            // Junio tiene 30 días; pasar 31 → 30 (martes) 
            var result = _svc.AdjustPaymentDate(2025, 6, 31);
            Assert.Equal(new DateTime(2025, 6, 30), result);
        }
    }
}