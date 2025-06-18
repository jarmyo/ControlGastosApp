using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;

namespace ControlGastos.Mobile.ViewModels
{
    public partial class PaymentsViewModel : ObservableObject
    {
        readonly IRecurringExpenseRepository _expRepo;
        readonly IPaymentRepository _payRepo;
        readonly ICalendarService _calendarService;

        public ObservableCollection<PaymentItem> PendingPayments { get; } = new();

        public PaymentsViewModel(
            IRecurringExpenseRepository expRepo,
            IPaymentRepository payRepo,
            ICalendarService calendarService)
        {
            _expRepo = expRepo;
            _payRepo = payRepo;
            _calendarService = calendarService;
        }

        [RelayCommand]
        public async Task LoadDataAsync()
        {
            PendingPayments.Clear();
            var today = DateTime.Today;
            var year = today.Year;
            var month = today.Month;

            var all = await _expRepo.GetAllAsync();
            var pagos = await _payRepo.GetByMonthAsync(year, month);
            var paidIds = pagos.Select(p => p.RecurringExpenseId).ToHashSet();

            foreach (var e in all.Where(x => !paidIds.Contains(x.Id)))
            {
                var amt = e.Type == ExpenseType.Fixed
                          ? e.FixedAmount ?? 0
                          : e.ApproximateAmount ?? 0;
                var due = _calendarService.AdjustPaymentDate(year, month, e.DayOfPayment);

                PendingPayments.Add(new PaymentItem
                {
                    Id = e.Id,
                    Name = e.Name,
                    Amount = amt,
                    DueDate = due
                });
            }
        }

        [RelayCommand]
        public async Task MarkAsync(int id)
        {
            var itm = PendingPayments.FirstOrDefault(x => x.Id == id);
            if (itm == null) return;

            await _payRepo.AddAsync(new Payment
            {
                RecurringExpenseId = id,
                Amount = itm.Amount,
                PaymentDate = DateTime.Today
            });
            await LoadDataAsync();
        }

        public class PaymentItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public decimal Amount { get; set; }
            public DateTime DueDate { get; set; }
        }
    }
}
