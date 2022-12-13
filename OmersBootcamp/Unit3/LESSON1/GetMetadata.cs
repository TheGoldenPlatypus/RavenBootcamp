using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit3.LESSON1
{
    /*
    UNIT 3 LESSON 1+2 EX 1.
    Notes:
        Revisions -  a nickname for a snapshot of documents that is taken every time the document changes. (allows us to track history).
        Metadata - a document which exists for inner use of the DB. it stores additional information about the documents

    purpose:  1) 
              2) 
    */
    class GetMetadata
    {
        static void Main()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                //  First we are asking for an instance of the document
                var product = session.Load<Product>("products/1-A");
                // Then, using this instance, we get the metadata.
                var metadata = session.Advanced.GetMetadataFor(product);

                // print document's metadata fields
                foreach (var info in metadata)
                {
                    Console.WriteLine($"{info.Key}: {info.Value}");
                }

                // adding a property to the metadata example
                metadata["last-modified-by"] = "Bagheera";
                session.SaveChanges();
            }
        }
    }
    // no fields nor properties since this class just used to load the metadata
    class Product { }
}
