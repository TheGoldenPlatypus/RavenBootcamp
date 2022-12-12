using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp
{
    // UNIT 1 LESSON 3 EX 2
    // purpose: to learn how to fetch a document and store its attributes into model class (fixed object)
    internal class UsingModelClasses
    {
        public static void Main(string[] args)
        {
            var documentStore = new DocumentStore
            {   
                Urls = new[] { "http://localhost:8080" },
                Database = "Northwind"
            };

            documentStore.Initialize();

            using (var session = documentStore.OpenSession())
            {
                // using the model class Product to unmarshall the document
                var p = session.Load<Product>("products/25-A");
                System.Console.WriteLine(p.Name);
            }
        }
  
    }
    
}
