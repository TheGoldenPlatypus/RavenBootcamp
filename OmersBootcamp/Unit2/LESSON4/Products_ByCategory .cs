using Raven.Client.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit2.LESSON4
{
    public class Products_ByCategory :
      AbstractIndexCreationTask<Product, Products_ByCategory.Result>
    {
        public class Result
        {
            public string Category { get; set; }
            public int Count { get; set; }
        }

        public Products_ByCategory()
        {
            // will list all products as anonymously objects . each obj will hold its category and count.
            Map = products =>
                from product in products
                select new
                {
                    Category = product.Category,
                    Count = 1
                };

            // will take the resulted list and group the elements by their categories.
            // finally it will return its result which is a composite obj of 'category' and the nr. of products under the category.
            // NOTE: the reduce query must(!) return its result in the same format that it received it.
            //       why? It is required if you want to be able to split the results and combine them later on.
            //            In other words, if you need to run the reduce function over the results of the reduce function.
                Reduce = results =>
                from result in results
                group result by result.Category into g
                select new
                {
                    Category = g.Key,
                    Count = g.Sum(x => x.Count)
                };
        }
    }
}
