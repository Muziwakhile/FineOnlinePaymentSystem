using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class Amortization
    {
        [Key]
        public int AmortizationID { get; set; }

        [Required,Display(Name ="Days Over stayed")]
        public int DaysOverstayed { get; set; }

        [Required]
        public int Percent { get; set; }

        [Required,Column(TypeName ="Decimal(10,2)"),Display(Name = "Amortization Amount")]
        public decimal AmortizationAmount { get; set; }


        public int CaseID { get; set; }
        public virtual Case Case { get; set; }


        public int FineID { get; set; }
        public virtual Fine Fine { get; set; }


        public virtual List<FinePayment> FinePayments { get; set; }
    }
}
