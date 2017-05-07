using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace QualityBags.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string FirstName {get;set;}
        public string LastName  { get; set; }
        public string FullName {
            get {
                return FirstName + " " + LastName;
            }
        }
        public string PhoneMobile { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }


        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }
    }
}
