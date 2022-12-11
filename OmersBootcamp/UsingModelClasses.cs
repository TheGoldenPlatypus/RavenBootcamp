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
    
    // this class was copied and pasted here from the Studio 
    public class Product
    {
        public string Category { get; set; }
        public bool Discontinued { get; set; }
        public string Name { get; set; }
        public float PricePerUnit { get; set; }
        public string QuantityPerUnit { get; set; }
        public int ReorderLevel { get; set; }
        public string Supplier { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
    }
}
