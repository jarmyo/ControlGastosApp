using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ControlGastos.WebUI.Pages.Expenses
{
    public class EditModel : PageModel
    {
        private readonly IRecurringExpenseRepository _repo;
        private readonly ICategoryRepository _catRepo;
        public EditModel(IRecurringExpenseRepository repo, ICategoryRepository catRepo)
        {
            _repo = repo;
            _catRepo = catRepo;
        }

        [BindProperty]
        public RecurringExpense Expense { get; set; } = null!;
        public IList<Category> Categories { get; private set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Categories = (await _catRepo.GetAllAsync()).ToList();

            var e = await _repo.GetByIdAsync(id);
            if (e == null) return RedirectToPage("Index");
            Expense = e;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Categories = (await _catRepo.GetAllAsync()).ToList();

            if (!ModelState.IsValid) return Page();
            await _repo.UpdateAsync(Expense);
            return RedirectToPage("Index");
        }
    }
}
