using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CsharpAppWithJQuery.Data;
using CsharpAppWithJQuery.Models;
using System.Data.OleDb;

namespace CsharpAppWithJQuery.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly CsharpAppWithJQuery.Data.CsharpAppWithJQueryContext _context;
        private readonly string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Personal Projects\\CSharpASP.NET-JQuery\\CsharpAppWithJQuery\\LocalDb\\CustomersDb.mdb";


        public CreateModel(CsharpAppWithJQuery.Data.CsharpAppWithJQueryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Customer.Add(Customer);

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Customers (Name, Address, City, State, Zip) VALUES (?, ?, ?, ?, ?)";
                using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("Name", Customer.Name);
                    command.Parameters.AddWithValue("Address", Customer.Address);
                    command.Parameters.AddWithValue("City", Customer.City);
                    command.Parameters.AddWithValue("State", Customer.State);
                    command.Parameters.AddWithValue("Zip", Customer.Zip);


                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("Rows affected: " + rowsAffected);
                }
            }
                await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
