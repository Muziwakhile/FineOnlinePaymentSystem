using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class Officer
    {
        [Key]
        public int OfficerID { get; set; }

        [Required, Column(TypeName = "varchar(25)")]
        public string Name { get; set; }

        [Required, Column(TypeName = "varchar(25)")]
        public string Surname { get; set; }

        [Required]
        public int ForceNumber { get; set; }

        [Required]
        public long Contact { get; set; }

        public string UserID { get; set; }
        public virtual IdentityUser User { get; set; }


        public virtual List<Case> Cases { get; set; }
    }
}
