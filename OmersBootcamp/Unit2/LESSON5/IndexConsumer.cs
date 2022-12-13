using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit2.LESSON5
{
    class IndexConsumer
    {
        /*
         UNIT 2 LESSON 5 EX 1.
 
         */
        static void Main(string[] args)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
               
                var query = session.Query<Products_ByCategory.Result, Products_ByCategory>();
                // no need using the 'include' since we've modified the index (defined in Product_ByCategory.cs)
                // to load only the document we need rather all of them.

                //.Include(x => x.Category);
                var results = (
                from result in query
                select result
                ).ToList();

                foreach (var result in results)
                {
                    //var category = session.Load<Category>(result.Category);
                    //Console.WriteLine($"{category.Name} has {result.Count} items.");
                    Console.WriteLine($"{result.Category} has {result.Count} items.");
                }
                
                /*
                // each query produces statistics attributes, we can access those stats.
                var orders = (
                from order in session.Query<Order>().Statistics(out var stats)
                where order.Company == "companies/1-a"
                orderby order.OrderedAt
                select order
                ).ToList();

                Console.WriteLine($"Index used was: {stats.IndexName}");
                */
            }
        }

    }
}
