using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlGastos.Domain.Interfaces;

namespace ControlGastos.Mobile.ViewModels
{
    public partial class IncomesViewModel : ObservableObject
    {
        readonly IRecurringIncomeRepository _repo;

        public ObservableCollection<IncomeItem> Incomes { get; } = new();

        public IncomesViewModel(IRecurringIncomeRepository repo)
        {
            _repo = repo;
        }

        [RelayCommand]
        public async Task LoadDataAsync()
        {
            Incomes.Clear();
            var all = await _repo.GetAllAsync();
            foreach (var i in all)
            {
                Incomes.Add(new IncomeItem
                {
                    Id = i.Id,
                    Name = i.Name,
                    DayOfIncome = i.DayOfIncome,
                    FixedAmount = i.FixedAmount
                });
            }
        }

        [RelayCommand]
        public Task CreateAsync()
            => Shell.Current.GoToAsync(nameof(Views.IncomesPage));

        [RelayCommand]
        public Task EditAsync(int id)
            => Shell.Current.GoToAsync($"{nameof(Views.IncomesPage)}?id={id}");

        [RelayCommand]
        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            await LoadDataAsync();
        }

        public class IncomeItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public int DayOfIncome { get; set; }
            public decimal FixedAmount { get; set; }
        }
    }
}
