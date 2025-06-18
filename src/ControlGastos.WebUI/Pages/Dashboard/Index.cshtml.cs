using ControlGastos.Domain.Interfaces;
using ControlGastos.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlGastos.WebUI.Pages.Dashboard
{
    public class IndexModel(
        ICalculationService calcSvc,
        IRecurringExpenseRepository expRepo,
        IPaymentRepository payRepo,
        ICalendarService calSvc,
        IRecurringIncomeRepository incomeRepo) : PageModel
    {
        private readonly ICalculationService _calcSvc = calcSvc;
        private readonly IRecurringExpenseRepository _expRepo = expRepo;
        private readonly IPaymentRepository _payRepo = payRepo;
        private readonly ICalendarService _calSvc = calSvc;
        private readonly IRecurringIncomeRepository _incomeRepo = incomeRepo;

        public decimal TotalPaid { get; private set; }
        public decimal TotalPending { get; private set; }
        public decimal TotalIncome { get; private set; }

        public IEnumerable<RecurringExpense>? PendingExpenses { get; private set; }
        public DateTime AdjustedDate(int day) =>
            _calSvc.AdjustPaymentDate(DateTime.Today.Year, DateTime.Today.Month, day);

        public async Task OnGetAsync()
        {
            var year = DateTime.Today.Year;
            var month = DateTime.Today.Month;

            // Gastos
            TotalPaid = await _calcSvc.GetTotalPaidAsync(year, month);
            TotalPending = await _calcSvc.GetTotalPendingAsync(year, month);

            var allExp = await _expRepo.GetAllAsync();
            var pagos = await _payRepo.GetByMonthAsync(year, month);
            var pagadasIds = pagos.Select(p => p.RecurringExpenseId).ToHashSet();
            PendingExpenses = allExp.Where(e => !pagadasIds.Contains(e.Id));

            // Ingresos
            var allInc = await _incomeRepo.GetAllAsync();
            TotalIncome = allInc.Sum(i => i.FixedAmount);
        }
    }
}
