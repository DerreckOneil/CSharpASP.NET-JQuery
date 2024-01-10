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
    public class DetailsModel : PageModel
    {
        private readonly CsharpAppWithJQuery.Data.CsharpAppWithJQueryContext _context;
        private readonly string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Personal Projects\\CSharpASP.NET-JQuery\\CsharpAppWithJQuery\\LocalDb\\CustomersDb.mdb";


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
    }
}
