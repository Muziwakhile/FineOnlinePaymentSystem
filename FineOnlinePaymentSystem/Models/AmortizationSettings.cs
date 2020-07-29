using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class AmortizationSettings
    {
        [Key]
        public int AmortizationSettingsID { get; set; }

        [Required]
        public int PercentPerDay { get; set; }


        [Required, Display(Name = "Days before amortization")]
        public int DaysBeforeAmortization { get; set; }

        public int DaysToBeInJail { get; set; }
    }
}
