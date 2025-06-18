using ControlGastos.Domain.Interfaces;
using System.ComponentModel;

namespace ControlGastos.Mobile
{
public partial class DashboardViewModel
{
    private readonly ICalculationService _calc;
    public decimal TotalPaid { get; private set; }
    // demás propiedades...

    public DashboardViewModel(ICalculationService calc) => _calc = calc;

    public async Task LoadAsync()
    {
        var today = DateTime.Today;
        TotalPaid = await _calc.GetTotalPaidAsync(today.Year, today.Month);
    }
}
}