using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QualityBags.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName
        {
            get {
                return FirstName + " " + LastName;
            }
        }

        [Required,
        RegularExpression(@"[0-9]"),
         MaxLength(11),
         Display(Name = "Mobile Number")
            ]
        public string PhoneMobile { get; set; }
        [Display(Name = "Home Number")]
        public string PhoneHome { get; set; }
        [Display(Name = "Work Number")]
        public string PhoneWork { get; set; }
        [Required]
        public string Email { get; set; }

        public ICollection<Bag> Bags {get;set;}
    }
}
