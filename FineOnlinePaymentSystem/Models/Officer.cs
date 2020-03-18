using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class Officer
    {
        public int OfficerID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int FroceNumber { get; set; }
        public long Contact { get; set; }

        public string UserID { get; set; }
        public IdentityUser User { get; set; }
    }
}
