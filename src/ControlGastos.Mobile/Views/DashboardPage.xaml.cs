
using ControlGastos.Mobile.ViewModels;

namespace ControlGastos.Mobile.Views
{
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
        }
        public DashboardPage(DashboardViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            Loaded += async (_, __) => await vm.LoadAsync();
        }
    }
}