using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class CaseStatus
    {
        [Key]
        public int CaseStatusID { get; set; }

        [Required,Column(TypeName = "varchar(15)")]
        public string Name { get; set; }

        public virtual List<Case> Cases { get; set; }
    }
}
