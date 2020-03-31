using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class FinePayment
    {
        public int FinePaymentID { get; set; }
        public DateTime dateTime { get; set; }
        public float AmortizationAmount { get; set; }
        public float AmountPayable { get; set; }
        public Byte Attachment { get; set; }


        public int AmortizationID { get; set; }
        public Amortization Amortization { get; set; }

        public int RelativeID { get; set; }
        public Relative Relative { get; set; }

        public int FineID { get; set; }
        public Fine Fine { get; set; }
    }
}
