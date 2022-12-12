using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Documents.Indexes;

namespace OmersBootcamp.Unit2.LESSON3
{
        public class People_Search : AbstractMultiMapIndexCreationTask<People_Search.Result>
        {
        // the class Result are used to provide projections.
        // Projections are a way to collect several fields from a document, instead of working with the whole document.
        // 
        public class Result
            {
                public string SourceId { get; set; }
                public string Name { get; set; }
                public string Type { get; set; }
            }

        // You can define as many map functions as you need
        // Each map function is defined using the AddMap method and has to produce the same output type
        //
        public People_Search()
            {
                AddMap<Company>(companies =>
                    from company in companies
                    select new Result
                    {
                        SourceId = company.Id,
                        Name = company.Contact.Name,
                        Type = "Company's contact"
                    }
                    );

                AddMap<Supplier>(suppliers =>
                    from supplier in suppliers
                    select new Result
                    {
                        SourceId = supplier.Id,
                        Name = supplier.Contact.Name,
                        Type = "Supplier's contact"
                    }
                    );

                AddMap<Employee>(employees =>
                    from employee in employees
                    select new Result
                    {
                        SourceId = employee.Id,
                        Name = $"{employee.FirstName} {employee.LastName}",
                        Type = "Employee"
                    }
                    );

                // The Index method here was used to mark the Name property as Search which enables full text search with this field.
                Index(entry => entry.Name, FieldIndexing.Search);

                // The Store method was used to enable projections and to store that defined properties along with the Index.
                // Thereby, the return of searched data comes from the Index,
                // instead of having to load the document and get the fields from it.
                Store(entry => entry.SourceId, FieldStorage.Yes);
                Store(entry => entry.Name, FieldStorage.Yes);
                Store(entry => entry.Type, FieldStorage.Yes);
            }
        }
    
}
