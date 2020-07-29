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
        public decimal Amount { get; set; }


        [Display(Name = "Release Date"), DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        public int CaseID { get; set; }
        public virtual Case Case { get; set; }


        public int OffenderID { get; set; }
        public virtual Offender Offender { get; set; }


        public int FineStatusID { get; set; }
        public virtual FineStatus FineStatus { get; set; }


        public virtual List<Amortization> Amortizations { get; set; }
        public virtual List<FinePayment> FinePayments { get; set; }
    }
}
