﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CsharpAppWithJQuery.Data;
using CsharpAppWithJQuery.Models;

namespace CsharpAppWithJQuery.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly CsharpAppWithJQuery.Data.CsharpAppWithJQueryContext _context;

        public DetailsModel(CsharpAppWithJQuery.Data.CsharpAppWithJQueryContext context)
        {
            _context = context;
        }

        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = customer;
            }
            return Page();
        }
    }
}
