using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ControlGastos.WebUI.Pages.Incomes
{
    public class CreateModel : PageModel
    {
        private readonly IRecurringIncomeRepository _repo;
        public CreateModel(IRecurringIncomeRepository repo) => _repo = repo;

        [BindProperty]
        public RecurringIncome Income { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _repo.AddAsync(Income);
            return RedirectToPage("Index");
        }
    }
}
