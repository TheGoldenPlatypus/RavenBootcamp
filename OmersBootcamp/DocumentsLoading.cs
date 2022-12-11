using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static OmersBootcamp.SingletonDocumentStore;

namespace OmersBootcamp
{
    /*
    UNIT 1 LESSON 5 EX 1
    purpose:  1) Confirming that a document is loaded only once in a session.
              2) Loading Multiple Documents with a Single Load Call.
              3) Loading Related Documents in a Single Remote Call.
   */
    internal class DocumentsLoading
    {
        public static void Main(string[] args)
        {
            // no declaring and initializing of the DocumentStore (we use the Singleton class that we've defined)
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                // # purpose 1
                // load method allows us to load one document or more by passing the ID(s) as params
                // A document is loaded only once in a session -  no use in multiple calls to same doc
                var p1 = session.Load<Product>("products/1-A");
                var p2 = session.Load<Product>("products/1-A");
                Debug.Assert(ReferenceEquals(p1, p2)); // will show us that p1 and p2 are the same objects

                // # purpose 2
                var products = session.Load<Product>(new[] {
                    "products/1-A",
                    "products/2-A",
                    "products/3-A"
                });

                // # purpose 3
                // some docs have properties that are referencig to other docs.
                // for example: if you have a 'product' doc and it has a propperty 'category' that refers to another doc,
                //              spliting it by loading the 'product' doc and afterwards the 'category' doc will cost us a lot
                //              in terms of performence.
                //              solution: using 'Include' which will activte the following chain:
                //                        find a document with the requested ID -> read its Category property value ->
                //                        find a document with that ID -> send both documents back to the client.
                // conclusion: when the last line is excecuted, the document is in the session cache and no additional remote call is made.
                var p = session
                    .Include<Product>(x => x.Category)
                    .Load("products/1-A");
                var c = session.Load<Category>(p.Category);
            }
        }

    }
    public class Category
    {
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
