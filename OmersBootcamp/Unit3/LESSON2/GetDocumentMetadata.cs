using Raven.Client.Documents.Commands;
using Sparrow.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit3.LESSON2
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
                // The GetDocumentsCommand method exposes a low-level way to get the metadata from a potentially large document without loading it.
                var command = new GetDocumentsCommand(
                    "products/1-A", null, metadataOnly: true);
                session.Advanced.RequestExecutor.Execute(
                    command, session.Advanced.Context);
                var result = (BlittableJsonReaderObject)command.Result.Results[0];
                var metadata = (BlittableJsonReaderObject)result["@metadata"];

                foreach (var propertyName in metadata.GetPropertyNames())
                {
                    metadata.TryGet<object>(propertyName, out var value);
                    Console.WriteLine($"{propertyName}: {value}");
                }
            }
        }
        // no fields nor properties since this class just used to load the metadata
        class Product { }
    }
}
