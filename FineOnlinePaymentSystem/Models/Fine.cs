using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class Fine
    {
        public int FineID { get; set; }
        public float Amount { get; set; }


        public int CaseID { get; set; }
        public Case Case { get; set; }


        public int OffenderID { get; set; }
        public Offender Offender { get; set; }
    }
}
