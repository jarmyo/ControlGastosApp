﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ControlGastos.Domain.Entities;
using ControlGastos.Infrastructure.Data;

namespace ControlGastos.WebUI.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly ControlGastos.Infrastructure.Data.ControlGastosDbContext _context;

        public DetailsModel(ControlGastos.Infrastructure.Data.ControlGastosDbContext context)
        {
            _context = context;
        }

        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category is not null)
            {
                Category = category;

                return Page();
            }

            return NotFound();
        }
    }
}
