using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class Amortization
    {
        public int AmortizationID { get; set; }
        public int DaysOverstayed { get; set; }
        public int Percent { get; set; }
        public float AmortizationAmount { get; set; }


        public int CaseID { get; set; }
        public Case Case { get; set; }


        public int FineID { get; set; }
        public Fine Fine { get; set; }
    }
}
