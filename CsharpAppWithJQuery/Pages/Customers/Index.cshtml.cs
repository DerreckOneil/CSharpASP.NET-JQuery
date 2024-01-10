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
        public IList<Customer> Customer { get; set; } = new List<Customer>();


        public void OnGet()
        {

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Customers";

                int customerIndex = 0;
                using (OleDbCommand command = new OleDbCommand(query, connection))
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        int customerId = int.Parse(reader[0].ToString());
                        string name = reader[1].ToString();
                        string address = reader[2].ToString();
                        string city = reader[3].ToString();
                        string state = reader[4].ToString();
                        int zip = int.Parse(reader[5].ToString());

                        customer.Id = customerId;
                        customer.Name = name;
                        customer.Address = address;
                        customer.City = city;
                        customer.State = state;
                        customer.Zip = zip; 

                        Customer.Add(customer);

                        customerIndex++;
                    }

                    Console.WriteLine("No data found in the table");

                }
            }
        }
        /*
        public async Task OnGetAsync()
        {
            Customer = await _context.Customer.ToListAsync();
        }
        */
    }
}
