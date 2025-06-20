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
    public class IndexModel : PageModel
    {
        private readonly ControlGastos.Infrastructure.Data.ControlGastosDbContext _context;

        public IndexModel(ControlGastos.Infrastructure.Data.ControlGastosDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.ToListAsync();
        }
    }
}
