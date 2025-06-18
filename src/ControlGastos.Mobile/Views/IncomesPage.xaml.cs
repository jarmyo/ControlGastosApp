using ControlGastos.Mobile.ViewModels;

namespace ControlGastos.Mobile.Views;

public partial class IncomesPage : ContentPage
{
    IncomesViewModel ViewModel => BindingContext as IncomesViewModel;

    public IncomesPage(IncomesViewModel viewModel)
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
