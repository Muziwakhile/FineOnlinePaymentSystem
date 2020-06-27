using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class FinePaymentStatus
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual List<FinePayment> FinePayments { get; set; }
    }
}
