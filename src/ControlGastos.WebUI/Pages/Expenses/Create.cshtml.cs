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
        private readonly ICategoryRepository _catRepo;
        private readonly IExpenseInstallmentRepository _installRepo;

        public CreateModel(IRecurringExpenseRepository repo, ICategoryRepository catRepo, IExpenseInstallmentRepository installRepo)
        {
            _repo = repo;
            _catRepo = catRepo;
            _installRepo = installRepo;
        }

        [BindProperty]
        public RecurringExpense Expense { get; set; } = new();
        public IList<Category> Categories { get; private set; }
        public async Task OnGetAsync()
        {
            Categories = (await _catRepo.GetAllAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Categories = (await _catRepo.GetAllAsync()).ToList();

            if (!ModelState.IsValid)
                return Page();

            await _repo.AddAsync(Expense);

            await _installRepo.DeleteByExpenseAsync(Expense.Id);
            if (Expense.TotalOccurrences.HasValue)
            {
                for (int i = 1; i <= Expense.TotalOccurrences; i++)
                    await _installRepo.AddAsync(new ExpenseInstallment
                    {
                        RecurringExpenseId = Expense.Id,
                        SequenceNumber = i,
                        TotalSequences = Expense.TotalOccurrences.Value
                    });
            }
            return RedirectToPage("Index");
        }
    }
}
