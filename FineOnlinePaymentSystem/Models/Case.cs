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
        
        [Required,Column(TypeName ="varchar(max)"),Display(Name ="Case Description")]
        public string CaseDescription { get; set; }

        [Required, Column(TypeName = "varchar(100)"),Display(Name ="Crime Location")]
        public string CrimeLocation { get; set; }

        [Required, Display(Name = "Date Of Crime")]
        public DateTime DateOfCrime { get; set; }

        [Display(Name ="Date Of Arrest")]
        public DateTime? DateOfArrest { get; set; }

        [Display(Name ="Court Date")]
        public DateTime? CourtDate { get; set; }

        public int OfficerID { get; set; }
        public Officer Officer { get; set; }


        public int OffenceID { get; set; }
        public Offence Offence { get; set; }


        public ICollection<CaseOffenders> CaseOffenders { get; set; }

    }
}
