using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityBags.Models
{
    public class Bag
    {
        public int BagID { get; set; }
        public string BagName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public int CategoryID { get; set; }
        public int SupplierID { get; set; }

        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
    }
}
