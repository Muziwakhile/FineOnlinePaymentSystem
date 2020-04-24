using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class Case
    {
        [Key]
        public int CaseID { get; set; }

        [Required, Display(Name ="Case Number")]
        public int CaseNumber { get; set; }
        
        [Required,Column(TypeName ="varchar(max)"),Display(Name ="Case Description"), MaxLength(50000)]
        public string CaseDescription { get; set; }

        [Required, Column(TypeName = "varchar(100)"),Display(Name ="Crime Location")]
        public string CrimeLocation { get; set; }

        [Required, Display(Name = "Date Of Crime"), DataType(DataType.Date)]
        public DateTime DateOfCrime { get; set; }

        [Display(Name ="Date Of Arrest"), DataType(DataType.Date)]
        public DateTime? DateOfArrest { get; set; }

        [Display(Name ="Court Date"), DataType(DataType.Date)]
        public DateTime? CourtDate { get; set; }

        public int OfficerID { get; set; }
        public virtual Officer Officer { get; set; }


        public int OffenceID { get; set; }
        public virtual Offence Offence { get; set; }

        public int CaseStatusID { get; set; }
        public virtual CaseStatus CaseStatus { get; set; }


        public virtual ICollection<CaseOffender> CaseOffenders { get; set; }
        public virtual List<Fine> Fines { get; set; }
        public virtual List<Amortization> Amortizations { get; set; }

    }
}
