using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QualityBags.Models
{
    public class Bag
    {
        public int BagID { get; set; }
        [Required,Display(Name ="Bag Name")]
        public string BagName { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Display(Name ="Image")]
        public string ImagePath { get; set; }

        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int SupplierID { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
    }
}
