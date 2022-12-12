using Raven.Client.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit2
{
    /*
    UNIT 2 LESSON 2 EX 1.
    Notes:
        Index -   a data structure which the DB engine uses to perform all queries.
        Map function - a function which is responsible for converting a document in JSON format into an index entry.
                       the function extracts pieces of data you will be willing to search on from the documents.
        Index naming convention - {collection name}/By {selected fields} Of {filtering criteria}. e.g. "Employees/ByFirstAndLastName"
    
    purpose:  1) creating an index using the C# API.
              2) specifying an index that should be used while querying
    */
    class IndexCreate
    {
        static void Main()
        {
            var store = DocumentStoreHolder.Store;
            // the class Employees_ByFirstAndLastName just defines the index.
            // execute command will actually create the Index
            new Employees_ByFirstAndLastName().Execute(store);

            // # purpose 2
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var results = session
                    .Query<Employee, Employees_ByFirstAndLastName>()
                    .Where(x => x.FirstName == "Robert")
                    .ToList();

                foreach (var employee in results)
                {
                    Console.WriteLine($"{employee.LastName}, {employee.FirstName}");
                }
            }
        }
    }

    // # purpose 1
    // The class name Employees_ByFirstAndLastName, by convention, will generate an index named Employees/ByFirstAndLastName.
    // The class inherits from AbstractIndexCreationTask<Employee>, which indicates this class as an index that operates on the Employees collection.
    public class Employees_ByFirstAndLastName : AbstractIndexCreationTask<Employee>
    {
        public Employees_ByFirstAndLastName()
        {
            Map = (employees) =>
                from employee in employees
                select new
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName
                };
        }
    }
}
