using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class FinePayment
    {
        [Key]
        public int FinePaymentID { get; set; }

        [Required]
        public DateTime Paymentdate { get; set; }

        [Required,Column(TypeName = "Decimal(10,2)"),Display(Name ="Amortization Amount")]
        public decimal AmortizationAmount { get; set; }

        [Required,Display(Name ="Amount Payable"),Column(TypeName ="Decimal(10,2)")]
        public decimal AmountPayable { get; set; }

        [Required,Display(Name ="Proof of Payment")]
        public byte[] Attachment { get; set; }



        public int AmortizationID { get; set; }
        public virtual Amortization Amortization { get; set; }

        public int RelativeID { get; set; }
        public virtual Relative Relative { get; set; }

        public int FineID { get; set; }
        public virtual Fine Fine { get; set; }

        public int FinePaymentStatusID { get; set; }
        public virtual FinePaymentStatus FinePaymentStatus { get; set; }
    }
}
