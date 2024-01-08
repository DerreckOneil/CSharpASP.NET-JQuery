using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CsharpAppWithJQuery.Data;
using CsharpAppWithJQuery.Models;
using System.Data.OleDb;

namespace CsharpAppWithJQuery.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly CsharpAppWithJQuery.Data.CsharpAppWithJQueryContext _context;
        private readonly string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Personal Projects\\CSharpASP.NET-JQuery\\CsharpAppWithJQuery\\LocalDb\\CustomersDb.mdb";

        public IndexModel(CsharpAppWithJQuery.Data.CsharpAppWithJQueryContext context)
        {
            _context = context;
        }
        public void OnGet()
        {

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT TOP 1 Name FROM Customers";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string columnVal = reader["Name"].ToString();
                        Console.WriteLine("database first element " + columnVal);
                    }
                    else
                    {
                        Console.WriteLine("No data found in the table");
                    }

                }
            }
        }
        public IList<Customer> Customer { get;set; } = default!;
        /*
        public async Task OnGetAsync()
        {
            Customer = await _context.Customer.ToListAsync();
        }
        */
    }
}
