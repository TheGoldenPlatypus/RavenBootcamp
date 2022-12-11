using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp
{    /*
     UNIT 1 LESSON 4 EX 1
     purpose: implement DocumentStore object as single and uniqe of its kind per application.
              to apply it, we are using the Singleton design pattern.
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

        public static class DocumentStoreHolder
        {
            // Lazy initialization of an object means that its creation is deferred until it is first used.
            // the main reason to use it, is with 'expensive' objects that unusing them can watse system resources for nothing.
            // it's also good to use if you don't want to worry about thread safety issues.
            private static readonly Lazy<IDocumentStore> LazyStore =
                new Lazy<IDocumentStore>(() =>
                {
                    var store = new DocumentStore
                    {
                        Urls = new[] { "http://localhost:8080" },
                        Database = "Northwind"
                    };

                    return store.Initialize();
                });
            // exposing the object for use 
            public static IDocumentStore Store =>
                LazyStore.Value;
        }
    }
}
