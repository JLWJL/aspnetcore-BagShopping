using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace QualityBags.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Bag Bag { get; set; }
    }
}
