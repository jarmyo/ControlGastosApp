using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;

namespace ControlGastos.Mobile.ViewModels
{
    public partial class ExpensesViewModel : ObservableObject
    {
        readonly IRecurringExpenseRepository _repo;

        public ObservableCollection<ExpenseItem> Expenses { get; } = new();

        public ExpensesViewModel(IRecurringExpenseRepository repo)
        {
            _repo = repo;
        }

        [RelayCommand]
        public async Task LoadDataAsync()
        {
            Expenses.Clear();
            var all = await _repo.GetAllAsync();
            foreach (var e in all)
            {
                var amt = e.Type == ExpenseType.Fixed
                          ? e.FixedAmount ?? 0
                          : e.ApproximateAmount ?? 0;

                Expenses.Add(new ExpenseItem
                {
                    Id = e.Id,
                    Name = e.Name,
                    Type = e.Type.ToString(),
                    DayOfPayment = e.DayOfPayment,
                    Amount = amt
                });
            }
        }

        [RelayCommand]
        public Task CreateAsync()
            => Shell.Current.GoToAsync(nameof(Views.ExpensesPage));

        [RelayCommand]
        public Task EditAsync(int id)
            => Shell.Current.GoToAsync($"{nameof(Views.ExpensesPage)}?id={id}");

        [RelayCommand]
        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            await LoadDataAsync();
        }

        public class ExpenseItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string Type { get; set; } = "";
            public int DayOfPayment { get; set; }
            public decimal Amount { get; set; }
        }
    }
}
