using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace OmersBootcamp
{
    /*
    UNIT 1 LESSON 6 EX 1
    purpose:  1) demonstrating Querying using LINQ via the Query Session Method
              2) demonstrating Querying using RQL 
    linked classes: CompanyExplorer.cs
    */

    // important note: Queries in RavenDB don't behave like queries in relational databases.
    // RavenDB does not allow computation during queries and it doesn't have problems with
    // table scans because all queries are already indexed for us
    class CompanyExplorer
    {
        static void Main(string[] args)
        {
            while (true)
            {
                WriteLine("Please, enter a company id (0 to exit): ");

                if (!int.TryParse(ReadLine(), out var companyId))
                {
                    WriteLine("Company # is invalid.");
                    continue;
                }

                if (companyId == 0) break;

                //   QueryCompanyOrdersLINQ(companyId);
                     QueryCompanyOrdersRQL(companyId);
            }

            WriteLine("Goodbye!");
        }

        // # purpose 1
        private static void QueryCompanyOrdersLINQ(int companyId)
        {
            string companyReference = $"companies/{companyId}-A";

            using (var session = DocumentStoreHolder.Store.OpenSession())
             {
                // listing down all orders made by a given company (a list will be held by 'orders' var)
                var orders = (
                    from order in session.Query<Order>()
                                            .Include(o => o.Company)
                    where order.Company == companyReference
                    select order
                    ).ToList();

                var company = session.Load<Company>(companyReference);

                if (company == null)
                {
                    WriteLine("Company not found.");
                    return;
                }

                WriteLine($"Orders for {company.Name}");
                
                foreach (var order in orders)
                {
                    WriteLine($"{order.Id} - {order.OrderedAt}");
                }
            }
        }

        // # purpose 2
        private static void QueryCompanyOrdersRQL(int companyId)
        {
            string companyReference = $"companies/{companyId}-A";

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var orders = session.Advanced.RawQuery<Order>(
                    "from Orders " +
                    "where Company== $companyId " +
                    "include Company"
                ).AddParameter("companyId", companyReference)
                .ToList();

                var company = session.Load<Company>(companyReference);

                if (company == null)
                {
                    WriteLine("Company not found.");
                    return;
                }

                WriteLine($"Orders for {company.Name}");

                foreach (var order in orders)
                {
                    WriteLine($"{order.Id} - {order.OrderedAt}");
                }
            }
        }
    }
}
