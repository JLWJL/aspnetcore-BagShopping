using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityBags.Models
{
    public class CartItem
    {
        public int ID { get; set; }

        //ID referring to ShoppingCart
        public string CartID { get; set; }

        public int Count { get; set; }
        public DateTime DateCreated { get; set; }

        public Bag Bag { get; set; }

    }
}
