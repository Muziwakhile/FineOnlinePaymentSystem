using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class AmortizationSettings
    {
        public int AmortizationSettingsID { get; set; }
        public int PercentPerDay { get; set; }
        public int DaysBeforeAmortization { get; set; }
    }
}
