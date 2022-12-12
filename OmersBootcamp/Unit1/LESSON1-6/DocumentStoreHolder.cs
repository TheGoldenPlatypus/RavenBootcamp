using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp
{
    // purpose: implement DocumentStore object as single and unique of its kind per application.
    // to apply it, we are using the Singleton design pattern
    public static class DocumentStoreHolder
    {
        // northwind DB
        // Lazy initialization of an object means that its creation is deferred until it is first used.
        // the main reason to use it, is with 'expensive' objects that non using them can waste system resources for nothing.
        // it's also good to use if you don't want to worry about thread safety issues
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
        // exposing the object to use 
        public static IDocumentStore Store =>
            LazyStore.Value;
    }
}
