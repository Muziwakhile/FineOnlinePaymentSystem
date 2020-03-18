using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class Relative
    {
        public int RelativeID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PIN { get; set; }
        public string Contact { get; set; }


        public string IdentityUserID { get; set; }
        public IdentityUser IdentityUser { get; set; }


    }
}
