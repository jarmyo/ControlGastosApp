using ControlGastos.Mobile.ViewModels;

namespace ControlGastos.Mobile.Views
{
    public partial class DashboardPage : ContentPage
    {
        DashboardViewModel ViewModel => BindingContext as DashboardViewModel;

        public DashboardPage(DashboardViewModel viewModel)
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
}