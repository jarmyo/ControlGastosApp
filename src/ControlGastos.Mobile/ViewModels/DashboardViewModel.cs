using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;

namespace ControlGastos.Mobile.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        readonly IRecurringExpenseRepository _expenseRepo;
        readonly IPaymentRepository _paymentRepo;
        readonly IRecurringIncomeRepository _incomeRepo;
        readonly ICalendarService _calendarService;
        readonly ICalculationService _calculationService;

        [ObservableProperty]
        decimal totalIncome;

        [ObservableProperty]
        decimal totalPaid;

        [ObservableProperty]
        decimal totalPending;

        public ObservableCollection<PendingExpenseItem> PendingExpenses { get; } = new();

        public DashboardViewModel(
            IRecurringExpenseRepository expenseRepo,
            IPaymentRepository paymentRepo,
            IRecurringIncomeRepository incomeRepo,
            ICalendarService calendarService,
            ICalculationService calculationService)
        {
            _expenseRepo = expenseRepo;
            _paymentRepo = paymentRepo;
            _incomeRepo = incomeRepo;
            _calendarService = calendarService;
            _calculationService = calculationService;
        }

        [RelayCommand]
        public async Task LoadDataAsync()
        {
            var today = DateTime.Today;
            var year = today.Year;
            var month = today.Month;

            // Total ingresos
            var incs = await _incomeRepo.GetAllAsync();
            totalIncome = incs.Sum(i => i.FixedAmount);

            // Totales gastos
            totalPaid = await _calculationService.GetTotalPaidAsync(year, month);
            totalPending = await _calculationService.GetTotalPendingAsync(year, month);

            // Pendientes
            PendingExpenses.Clear();
            var all = await _expenseRepo.GetAllAsync();
            var pagos = (await _paymentRepo.GetByMonthAsync(year, month))
                         .Select(p => p.RecurringExpenseId)
                         .ToHashSet();

            foreach (var e in all.Where(x => !pagos.Contains(x.Id)))
            {
                var amt = e.Type == ExpenseType.Fixed
                          ? e.FixedAmount ?? 0
                          : e.ApproximateAmount ?? 0;
                var due = _calendarService.AdjustPaymentDate(year, month, e.DayOfPayment);

                PendingExpenses.Add(new PendingExpenseItem
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
            var item = PendingExpenses.FirstOrDefault(x => x.Id == id);
            if (item == null) return;

            await _paymentRepo.AddAsync(new Payment
            {
                RecurringExpenseId = id,
                Amount = item.Amount,
                PaymentDate = DateTime.Today
            });
            await LoadDataAsync();
        }

        public class PendingExpenseItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public decimal Amount { get; set; }
            public DateTime DueDate { get; set; }
        }
    }
}