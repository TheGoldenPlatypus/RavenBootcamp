using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit2
{
    /*
        UNIT 2 LESSON 1 EX 1.

    */
    class IndexingSample
    {
        static void Main()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var ordersIds = (
                    from order in session.Query<Order>()
                    where order.Company == "companies/1-A"
                    orderby order.OrderedAt
                    select order.Id
                    ).ToList();

                foreach (var id in ordersIds)
                {
                    Console.WriteLine(id);
                }
            }
        }
    }

}
