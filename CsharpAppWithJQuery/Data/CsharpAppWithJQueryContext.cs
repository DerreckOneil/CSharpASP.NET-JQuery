using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CsharpAppWithJQuery.Models;

namespace CsharpAppWithJQuery.Data
{
    public class CsharpAppWithJQueryContext : DbContext
    {
        public CsharpAppWithJQueryContext (DbContextOptions<CsharpAppWithJQueryContext> options)
            : base(options)
        {
        }

        public DbSet<CsharpAppWithJQuery.Models.Customer> Customer { get; set; } = default!;
    }
}
