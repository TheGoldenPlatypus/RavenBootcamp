using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit2.LESSON4._2
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

    public class Order
    {
        public string OrderedAt { get; set; }
        public string Employee { get; set; }
    }
}
