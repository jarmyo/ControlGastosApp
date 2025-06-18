using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ControlGastos.WebUI.Pages.Incomes
{
    public class EditModel : PageModel
    {
        private readonly IRecurringIncomeRepository _repo;
        public EditModel(IRecurringIncomeRepository repo) => _repo = repo;

        [BindProperty]
        public RecurringIncome Income { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var i = await _repo.GetByIdAsync(id);
            if (i == null) return RedirectToPage("Index");
            Income = i;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _repo.UpdateAsync(Income);
            return RedirectToPage("Index");
        }
    }
}
