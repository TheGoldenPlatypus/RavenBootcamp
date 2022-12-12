using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit2.LESSON4_2
{
    class IndexConsumer
    {
        static void Main(string[] args)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var query = session
                    .Query<Employee_BySalesInSingleMonth.Result, Employee_BySalesInSingleMonth>()
                    .Include(x => x.Employee);

                var results = (
                    from result in query
                    where result.Month == "1998-03"
                    orderby result.TotalSales descending
                    select result
                    ).ToList();

                foreach (var result in results)
                {
                    var employee = session.Load<Employee>(result.Employee);
                    Console.WriteLine(
                        $"{employee.FirstName} {employee.LastName}"
                        + $" made {result.TotalSales} sales.");
                }
            }
        }
    }
}
