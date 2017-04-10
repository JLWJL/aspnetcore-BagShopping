using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityBags.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public int Contact { get; set; }
        public string Email { get; set; }

        public ICollection<Bag> Bags {get;set;}
    }
}
