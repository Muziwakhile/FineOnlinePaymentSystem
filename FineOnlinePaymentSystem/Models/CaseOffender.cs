using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class CaseOffender
    {
       
        public int CaseID { get; set; }
        public virtual Case Case { get; set; }

        public int OffenderID { get; set; }
        public virtual Offender Offender { get; set; }
    }
}
