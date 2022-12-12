using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Raven.Client.Documents;

namespace OmersBootcamp
{
    /*
        UNIT 1 LESSON 5 EX 2
        purpose:  1) Confirming that a document is loaded only once in a session.
                
    */
    class OrderExplorer
    {
        static void Main(string[] args)
        {
            // User interface which requests order numbers
            while (true)
            {
                WriteLine("Please, enter an order # (0 to exit): ");

                int orderNumber;
                if (!int.TryParse(ReadLine(), out orderNumber))
                {
                    WriteLine("Order # is invalid.");
                    continue;
                }

                if (orderNumber == 0) break;

                PrintOrder(orderNumber);
            }

            WriteLine("Goodbye!");
        }

        // load and print order data
        // self-note: In lambda expressions, the lambda operator => separates the input parameters on
        // the left side from the lambda body on the right side.
        private static void PrintOrder(int orderNumber)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                // Include function ensures that all data is returned from the server in a single response.
                var order = session
                    .Include<Order>(o => o.Company)
                    .Include(o => o.Employee)
                    .Include(o => o.Lines.Select(l => l.Product))
                    .Load($"orders/{orderNumber}-A");

                if (order == null)
                {
                    WriteLine($"Order #{orderNumber} not found.");
                    return;
                }

                WriteLine($"Order #{orderNumber}");

                var c = session.Load<Company>(order.Company);
                WriteLine($"Company : {c.Id} - {c.Name}");

                var e = session.Load<Employee>(order.Employee);
                WriteLine($"Employee: {e.Id} - {e.LastName}, {e.FirstName}");

                foreach (var orderLine in order.Lines)
                {
                    var p = session.Load<Product>(orderLine.Product);
                    WriteLine($"   - {orderLine.ProductName}," +
                              $" {orderLine.Quantity} x {p.QuantityPerUnit}");
                }
            }
        }

    }
}
