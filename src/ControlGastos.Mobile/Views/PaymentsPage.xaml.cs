using ControlGastos.Mobile.ViewModels;

namespace ControlGastos.Mobile.Views;

public partial class PaymentsPage : ContentPage
{
    PaymentsViewModel ViewModel => BindingContext as PaymentsViewModel;

    public PaymentsPage(PaymentsViewModel viewModel)
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