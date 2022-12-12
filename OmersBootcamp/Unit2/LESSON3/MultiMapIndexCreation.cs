using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit2.LESSON3
{
    /*
    UNIT 2 LESSON 3 EX 1.
    Notes:
        MultiMap Index -  allows us search docs from multiple collections.

    purpose:  1) 
              2) 
    */
    class MultiMapIndexCreation
    {
        static void Main(string[] args)
        {
            Console.Title = "Multi-map sample";
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                while (true)
                {
                    Console.Write("\nSearch terms: ");
                    var searchTerms = Console.ReadLine();

                    foreach (var result in Search(session, searchTerms))
                    {
                        Console.WriteLine($"{result.SourceId}\t{result.Type}\t{result.Name}");
                    }
                }
            }
        }

        public static IEnumerable<People_Search.Result> Search(
            IDocumentSession session,
            string searchTerms
        )
                {
                    var results = session.Query<People_Search.Result, People_Search>()
                        .Search(
                            r => r.Name,
                            searchTerms
                        )
                        .ProjectInto<People_Search.Result>()
                        .ToList();

                    return results;
                }
    }
}
