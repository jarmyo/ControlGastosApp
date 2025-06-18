using ControlGastos.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class RecurringExpense
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public ExpenseType Type { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? FixedAmount { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? ApproximateAmount { get; set; }

    [Range(1, 31)]
    public int DayOfPayment { get; set; }
}
