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
        public int OrderID { get; set; }
        public int CustomerID {get;set;}
        public float SubTotal { get; set; }
        public float TotalCost { get; set; }

        [DataType(DataType.Date)] //Week 4
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set;}
        public OrderStatus Status { get; set; }

        //Navigation property
        public ICollection<OrderItem> OrderItems { get; set; }
        public User Customer { get; set; }

    }
}
