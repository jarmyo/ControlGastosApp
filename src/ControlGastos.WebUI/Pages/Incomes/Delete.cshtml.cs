using ControlGastos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ControlGastos.WebUI.Pages.Incomes
{
    public class DeleteModel : PageModel
    {
        private readonly IRecurringIncomeRepository _repo;
        public DeleteModel(IRecurringIncomeRepository repo) => _repo = repo;

        [BindProperty]
        public int IncomeId { get; set; }
        public string? Name { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var i = await _repo.GetByIdAsync(id);
            if (i == null) return RedirectToPage("Index");
            IncomeId = i.Id;
            Name = i.Name;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _repo.DeleteAsync(IncomeId);
            return RedirectToPage("Index");
        }
    }
}
