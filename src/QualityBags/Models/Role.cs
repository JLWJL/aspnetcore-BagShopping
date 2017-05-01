using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityBags.Models
{
    public enum RoleDescription
    {
        Admin = 0, Customer
    }
    public class Role
    {
        public int RoleID { get; set; }
        public RoleDescription Description { get; set; }//How define either'admin' or 'customer'??

        //Navigation property
        public ICollection<User> Users { get; set; }
    }
}
