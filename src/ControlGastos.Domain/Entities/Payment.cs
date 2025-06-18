namespace ControlGastos.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int RecurringExpenseId { get; set; }
        public RecurringExpense RecurringExpense { get; set; } = null!;

        // Fecha en la que se realizó el pago
        public DateTime PaymentDate { get; set; }

        // Monto exacto que se pagó
        public decimal Amount { get; set; }
    }
}

