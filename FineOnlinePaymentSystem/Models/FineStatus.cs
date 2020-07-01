using System.Collections.Generic;

namespace FineOnlinePaymentSystem.Models
{
    public class FineStatus
    {
        public int FineStatusID { get; set; }
        public string Name { get; set; }


        public virtual List<Fine> Fines { get; set; }
    }
}