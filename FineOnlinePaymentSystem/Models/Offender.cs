using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class Offender
    {
        [Key]
        public int OffenderID { get; set; }

        [Required, Column(TypeName = "varchar(25)")]
        public string Name { get; set; }

        [Required, Column(TypeName = "varchar(25)")]
        public string Surname { get; set; }

        [Required, Column(TypeName = "varchar(18)")]
        public string PIN { get; set; }

        [Required]
        public int Age { get; set; }

        [Required, Column(TypeName = "varchar(25)")]
        public string Nationality { get; set; }

        [Required, Column(TypeName = "varchar(100)")]
        public string HomeAddress { get; set; }


        public int StatusID { get; set; }
        public OffenderStatus Status { get; set; }


        public ICollection<CaseOffender> CaseOffenders { get; set; }
    }
}
