using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class Fine
    {
        [Key]
        public int FineID { get; set; }

        [Required,Column(TypeName ="Decimal(10,2)")]
        public float Amount { get; set; }


        public int CaseID { get; set; }
        public Case Case { get; set; }


        public int OffenderID { get; set; }
        public Offender Offender { get; set; }


        public virtual List<Amortization> Amortizations { get; set; }
    }
}
