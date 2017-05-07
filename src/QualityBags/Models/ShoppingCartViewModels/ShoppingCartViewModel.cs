using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityBags.Models.ShoppingCartViewModels
{
    public class ShoppingCartViewModel
    {
        public ICollection<CartItem> CartItems { get; set; }
        public decimal CartTotal { get; set; }
        public decimal GST { get { return 0.15M; } }
    }
}
