﻿using System;
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
        public float AmortizationAmount { get; set; }

        [Required,Display(Name ="Amount Payable"),Column(TypeName ="Decimal(10,2)")]
        public float AmountPayable { get; set; }

        [Required,Display(Name ="Proof of Payment")]
        public Byte Attachment { get; set; }


        public int AmortizationID { get; set; }
        public Amortization Amortization { get; set; }

        public int RelativeID { get; set; }
        public Relative Relative { get; set; }

        public int FineID { get; set; }
        public Fine Fine { get; set; }
    }
}
