using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.WebUI.Pages.Incomes
{
    public class IndexModel : PageModel
    {
        private readonly IRecurringIncomeRepository _repo;
        public IndexModel(IRecurringIncomeRepository repo) => _repo = repo;

        public IEnumerable<RecurringIncome> Incomes { get; private set; } = new List<RecurringIncome>();

        public async Task OnGetAsync()
        {
            Incomes = await _repo.GetAllAsync();
        }
    }
}
