using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ControlGastos.WebUI.Pages.Expenses
{
    public class CreateModel : PageModel
    {
        private readonly IRecurringExpenseRepository _repo;

        public CreateModel(IRecurringExpenseRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public RecurringExpense Expense { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _repo.AddAsync(Expense);
            return RedirectToPage("Index");
        }
    }
}
