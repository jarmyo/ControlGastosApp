using ControlGastos.Domain.Entities;
using ControlGastos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace ControlGastos.WebUI.Pages.Payments
{
    public class MarkModel : PageModel
    {
        private readonly IRecurringExpenseRepository _expRepo;
        private readonly IPaymentRepository _payRepo;
        private readonly ICalendarService _calSvc;

        public int ExpenseId { get; set; }
        public string Name { get; set; } = "";
        public ExpenseType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }

        [BindProperty]
        public decimal PaidAmount { get; set; }

        public MarkModel(
            IRecurringExpenseRepository expRepo,
            IPaymentRepository payRepo,
            ICalendarService calSvc)
        {
            _expRepo = expRepo;
            _payRepo = payRepo;
            _calSvc = calSvc;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var e = await _expRepo.GetByIdAsync(id);
            if (e == null) return RedirectToPage("/Dashboard/Index");

            ExpenseId = e.Id;
            Name = e.Name;
            Type = e.Type;
            Amount = e.Type == ExpenseType.Fixed ? e.FixedAmount!.Value : e.ApproximateAmount!.Value;
            DueDate = _calSvc.AdjustPaymentDate(DateTime.Today.Year, DateTime.Today.Month, e.DayOfPayment);
            PaidAmount = Amount;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _payRepo.AddAsync(new Payment
            {
                RecurringExpenseId = ExpenseId,
                Amount = PaidAmount,
                PaymentDate = DateTime.Today
            });
            return RedirectToPage("/Dashboard/Index");
        }
    }
}
