using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CsharpAppWithJQuery.Data;
using CsharpAppWithJQuery.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.OleDb;

namespace CsharpAppWithJQuery.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly CsharpAppWithJQuery.Data.CsharpAppWithJQueryContext _context;
        private readonly string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Personal Projects\\CSharpASP.NET-JQuery\\CsharpAppWithJQuery\\LocalDb\\CustomersDb.mdb";

        public EditModel(CsharpAppWithJQuery.Data.CsharpAppWithJQueryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Console.WriteLine("Id: " + id);
            if (id == null)
            {
                return NotFound();
            }

            Customer displayCustomer = new Customer();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();


                string query = "SELECT * FROM Customers WHERE CustomerId =" + id;

                using (OleDbCommand command = new OleDbCommand(query, connection))
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int customerId = Convert.ToInt32(reader["CustomerId"]);
                        string name = reader["Name"].ToString();
                        string address = reader["Address"].ToString();
                        string city = reader["City"].ToString();
                        string state = reader["State"].ToString();
                        int zip = Convert.ToInt32(reader["Zip"]);

                        displayCustomer.Id = customerId;
                        displayCustomer.Name = name;
                        displayCustomer.Address = address;
                        displayCustomer.City = city;
                        displayCustomer.State = state;
                        displayCustomer.Zip = zip;

                    }
                }
            }

            if (displayCustomer == null)
            {
                return NotFound();
            }
            else
                Customer = displayCustomer;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Customer).State = EntityState.Modified;

            Customer updatedCustomer = new Customer();
            Console.WriteLine("What's the customer ID here? " + Customer.Id); 
            string updateQuery = "UPDATE Customers SET Name =?, Address =?, City =?, State =?, Zip =? WHERE CustomerId = " + Customer.Id;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                using (OleDbCommand updateCommand = new OleDbCommand(updateQuery, connection))
                {
                    Console.WriteLine("new updated data perhaps? " + Customer.Name);
                    updateCommand.Parameters.AddWithValue("Name", Customer.Name);
                    updateCommand.Parameters.AddWithValue("Address", Customer.Address);
                    updateCommand.Parameters.AddWithValue("City", Customer.City);
                    updateCommand.Parameters.AddWithValue("State", Customer.State);
                    updateCommand.Parameters.AddWithValue("Zip", Customer.Zip);


                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    Console.WriteLine($"Affected rows: {rowsAffected}");
                }
            }

                

                return RedirectToPage("./Index");
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
