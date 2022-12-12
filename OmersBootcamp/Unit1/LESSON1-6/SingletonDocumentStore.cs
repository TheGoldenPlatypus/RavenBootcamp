using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp
{    /*
     UNIT 1 LESSON 4 EX 1.
     linked classes:  DocumentStoreHolder.cs
    */
    internal class SingletonDocumentStore
    {
        public static void Main(string[] args)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var p = session.Load<Product>("products/20-A");
                System.Console.WriteLine(p.Name);
            }
        }
    }
}
