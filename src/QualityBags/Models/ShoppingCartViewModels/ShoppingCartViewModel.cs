using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QualityBags.Models.ShoppingCartViewModels
{
    public class ShoppingCartViewModel
    {
        public ICollection<CartItem> CartItems { get; set; }

        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}")]
        public decimal CartTotal { get; set; }

        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}")]
        public decimal GST { get { return 0.15M; } }

        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}")]
        public decimal GSTPrice { get; set; }

        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}")]
        public decimal CartSubTotal { get; set; }

    }
}
