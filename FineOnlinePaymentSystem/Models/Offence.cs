using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class Offence
    {
        [Key]
        public int OffenseID { get; set; }

        [Required,Column(TypeName ="varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Description { get; set; }


        public virtual List<Case> Cases { get; set; }
    }
}
