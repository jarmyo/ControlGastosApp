using ControlGastos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ControlGastos.WebUI.Pages.Expenses
{
    public class DeleteModel : PageModel
    {
        private readonly IRecurringExpenseRepository _repo;
        public DeleteModel(IRecurringExpenseRepository repo) => _repo = repo;

        [BindProperty]
        public int ExpenseId { get; set; }
        public string? Name { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return RedirectToPage("Index");
            ExpenseId = e.Id;
            Name = e.Name;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _repo.DeleteAsync(ExpenseId);
            return RedirectToPage("Index");
        }
    }
}
