using Raven.Client.Documents;
using Raven.Client.ServerWide.Operations;
using Raven.Client.ServerWide;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Documents.Indexes;

namespace OmersBootcamp.Unit2.LESSON5

{
    public static class DocumentStoreHolder
    {

        private static readonly Lazy<IDocumentStore> LazyStore =
               new Lazy<IDocumentStore>(() =>
               {
                   var store = new DocumentStore
                   {
                       Urls = new[] { "http://localhost:8080" },
                       Database = "Northwind"
                   };

                   store.Initialize();

                   var asm = Assembly.GetExecutingAssembly();
                   //ask the client API to find all indexes classes automatically and send them all together to the server.
                   IndexCreation.CreateIndexes(asm, store);

                   // Try to retrieve a record of this database
                   var databaseRecord = store.Maintenance.Server.Send(new GetDatabaseRecordOperation(store.Database));

                   if (databaseRecord != null)
                       return store;

                   var createDatabaseOperation =
                       new CreateDatabaseOperation(new DatabaseRecord(store.Database));

                   store.Maintenance.Server.Send(createDatabaseOperation);

                   return store;
               });

        public static IDocumentStore Store =>
            LazyStore.Value;
    }
}
