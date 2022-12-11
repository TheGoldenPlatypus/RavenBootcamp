using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raven.Client.Documents;

namespace OmersBootcamp
{
    // UNIT 1 LESSON 3 EX 1
    // purpose: to learn how to setup db connection and fetch a document dynamiclly 
    internal class ClientAppExample
    {
        public static void Main(string[] args)
        {
            // DocumentStore obj acts as main client API object which establishes and
            // manages the connection channel between an application and a database instance 
            var documentStore = new DocumentStore
            {   // construct by URL and DB name
                Urls = new[] { "http://localhost:8080" },
                Database = "Northwind"
            };
            // after the 'initialize' invokation, the DocumentStore obj points at some db server instance (URL & dbName combination).
            documentStore.Initialize();

            // opening new session obj allows us to preform any operation on the DB
            using (var session = documentStore.OpenSession())
            {
                // load allows us to get a specific entity according to given document id (note:document ID is unique per database).
                // the 'dynamic' type represents an object that its operation are resolved at runtime (can be any obj)
                var p = session.Load<dynamic>("products/1-A");
                System.Console.WriteLine(p.Name);
            }
            System.Console.WriteLine("Press <ENTER> to continue...");
            System.Console.ReadLine();
        }

    }
}
