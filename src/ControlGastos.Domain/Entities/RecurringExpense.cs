using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Domain.Entities
{
    public class RecurringExpense
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        public ExpenseType Type { get; set; }
        public decimal? FixedAmount { get; set; }
        public decimal? ApproximateAmount { get; set; }
        [Range(1, 31)]
        public int DayOfPayment { get; set; }
        public bool IsDomiciled { get; set; } = false;
        public string? Notes { get; set; }
        [Range(1, int.MaxValue)]
        public int? TotalOccurrences { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}