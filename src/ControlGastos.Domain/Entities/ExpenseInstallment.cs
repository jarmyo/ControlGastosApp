using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Domain.Entities
{
    public class ExpenseInstallment
    {
        public int Id { get; set; }

        // FK al gasto
        public int RecurringExpenseId { get; set; }
        public RecurringExpense RecurringExpense { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int SequenceNumber { get; set; }

        [Range(1, int.MaxValue)]
        public int TotalSequences { get; set; }
    }
}