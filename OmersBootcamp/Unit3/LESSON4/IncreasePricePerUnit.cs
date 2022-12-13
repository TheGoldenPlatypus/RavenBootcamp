using Raven.Client.Documents.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit3.LESSON4
{
    /*
    UNIT 3 LESSON 4 EX 1.
    Notes:
        Batch ops - a name for ops that are to preform on large amounts of documents answering a certain criteria at once.
                    consider you need to increase the price of all products that are eatable in the DB. Using RavenDB we can do it by using 
                    the same queries and indexes that we are using for data retrieval.
    */
    class IncreasePricePerUnit
    {
        static void Main()
        {
            var operation = DocumentStoreHolder.Store
                .Operations
                .Send(new PatchByQueryOperation(@"from Products as p
                                where p.Discontinued = false
                                update
                                {
                                    p.PricePerUnit = p.PricePerUnit * 1.1
                                }"));
            operation.WaitForCompletion();
        }

    }
}
