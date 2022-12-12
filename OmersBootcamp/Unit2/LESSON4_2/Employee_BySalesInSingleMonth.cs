using OmersBootcamp.Unit2.LESSON4;
using Raven.Client.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit2.LESSON4_2
{
    public class Employee_BySalesInSingleMonth :
      AbstractIndexCreationTask<Order, Employee_BySalesInSingleMonth.Result>
    {
        public class Result
        {
            public string Month { get; set; }
            public string Employee { get; set; }

            public int TotalSales { get; set; }
        }

        public Employee_BySalesInSingleMonth()
        {
            Map = Orders =>
            from order in Orders
            select new
            {
                order.Employee,
                Month = order.OrderedAt.ToString("yyyy-MM"),
                TotalSales = 1
            };

            Reduce = Results =>
            from result in Results
            group result by new
            {
                result.Employee,
                result.Month
            }
            into g
            select new
            {
                g.Key.Employee,
                g.Key.Month,
                TotalSales = g.Sum(x => x.TotalSales)
            };
        }
    }
}
