using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit2.LESSON4
{
    class IndexConsumer
    {
        /*
         UNIT 2 LESSON 4 EX 1.
 
         */
        static void Main(string[] args)
        {
            // some queries to test our map-reduced index that we've defined in Products_ByCategory.cs 
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var results = session
                    .Query<Products_ByCategory.Result, Products_ByCategory>()
                    .Include(x => x.Category)
                    .ToList();

                foreach (var result in results)
                {
                    var category = session.Load<Category>(result.Category);
                    Console.WriteLine($"{category.Name} has {result.Count} items.");
                }
            }
        }

    }
}
