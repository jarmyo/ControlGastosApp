using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Domain.Entities
{
    public class RecurringIncome
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public decimal FixedAmount { get; set; }

        [Range(1, 31)]
        public int DayOfIncome { get; set; }
    }
}