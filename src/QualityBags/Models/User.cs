using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QualityBags.Models
{
    public class User
    {
        public int UserID { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required,
        RegularExpression(@"[0-9]"),
         StringLength(11),
         Display(Name ="Mobile Number")
            ]
        public string PhoneMobile { get; set; }
        [Display(Name = "Home Number")]
        public string PhoneHome { get; set; }
        [Display(Name = "Work Number")]
        public string PhoneWork { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }

        [Display(Name = "Role")]
        public int RoleID { get; set; }

        //Navigation property
        public Role Role { get; set; }

    }
}
