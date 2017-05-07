using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualityBags.Models
{
    public enum OrderStatus
    {
        Waiting = 0, Shipped
    }

    public class Order
    {
        private const decimal gst = 0.15M;

        public int OrderID { get; set; }

        public string CustomerID{ get; set; }
        
        [DisplayFormat(DataFormatString ="{0:C}")]
        public decimal SubTotal { get; set; }
        public decimal GST  {
            get { return gst; }
        }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalCost { get; set; }

        [DataType(DataType.Date), Display(Name ="Order Date")] //Week 4
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set;}

        [Required, Display(Name = "Receiver's Name")]
        public string Receiver { get; set; }
        [Required]
        public string Street{ get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Country { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public string Contact { get; set; }

        public OrderStatus Status { get; set; }

        //Navigation property
        public List<OrderItem> OrderItems { get; set; }
        public ApplicationUser Customer { get; set; }

        public string Address
        {
            get {
                return Street + ", " + City + " " + PostalCode + ", " + Country;
            } 
        }

    }
}
