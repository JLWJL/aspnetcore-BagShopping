using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace QualityBags.Models
{
    public class ApplicationUser:IdentityUser
    {
        public bool Enabled { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneMobile { get; set; }

        public string PhoneHome { get; set; }

        public string PhoneWork { get; set; }

        public string Address { get; set; }

        public ICollection<Order> Orders { get; set;}
    }
}
