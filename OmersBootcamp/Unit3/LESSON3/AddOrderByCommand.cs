using Raven.Client.Documents.Commands.Batches;
using Raven.Client.Documents.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit3.LESSON3
{
    /*
    UNIT 3 LESSON 3 EX 1.
    Notes:
        RavenDB Ops&Commands - Using Operations and Commands you can manipulate data and change configuration on a server.
                               although it sounds exactly like the 'session' obj, the 'session' obj is created as a high-level
                               interface mainly to preform LINQ queries. BUT if we want to do something in the low-level, like
                               Database Maintenance, Server Maintenance and Patching, so we should using the RDB operations&commands.
    */
    class AddOrderByCommand
    {
        static void Main()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                // using patch command to add new element (line) to the 'Lines' array aka. adding new product to existing order.
                // the Patch command which performs partial document updates without having to load, modify, and save a full document.

                session.Advanced.Patch<Order, OrderLine>("orders/816-A",
                     x => x.Lines,
                     lines => lines.Add(new OrderLine
                     {
                         Product = "products/1-a",
                         ProductName = "Chai",
                         PricePerUnit = 18M,
                         Quantity = 1,
                         Discount = 0
                     }));

                session.SaveChanges();
            }
        }
    }
    public class Order
    {
        public List<OrderLine> Lines { get; set; }
    }

    public class OrderLine
    {
        public string Product { get; set; }
        public string ProductName { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
