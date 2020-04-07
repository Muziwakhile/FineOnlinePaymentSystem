using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class Relative
    {
        [Key]
        public int RelativeID { get; set; }

        [Required, Column(TypeName = "varchar(25)")]
        public string Name { get; set; }

        [Required, Column(TypeName = "varchar(25)")]
        public string Surname { get; set; }

        [Required,Column(TypeName ="varchar(18)")]
        public string PIN { get; set; }

        [Required]
        public long Contact { get; set; }


        public string IdentityUserID { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }


        public virtual List<FinePayment> FinePayments { get; set; }


    }
}
