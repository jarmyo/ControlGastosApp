using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.WebUI.Pages.Expenses
{
    public class IndexModel : PageModel
    {
        private readonly IRecurringExpenseRepository _repo;

        public IndexModel(IRecurringExpenseRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<RecurringExpense> Expenses { get; private set; }

        public async Task OnGetAsync()
        {
            Expenses = await _repo.GetAllAsync();
        }
    }
}
