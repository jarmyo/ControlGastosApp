using ControlGastos.Mobile.ViewModels;

namespace ControlGastos.Mobile.Views;

public partial class ExpensesPage : ContentPage
{
    ExpensesViewModel ViewModel => BindingContext as ExpensesViewModel;

    public ExpensesPage(ExpensesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ViewModel.LoadDataAsync();
    }
}
