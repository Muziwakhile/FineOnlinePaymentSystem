using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class Offender
    {
        public int OffenderID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PIN { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string HomeAddress { get; set; }


        public int StatusID { get; set; }
        public OffenderStatus Status { get; set; }


        public ICollection<CaseOffenders> CaseOffenders { get; set; }
    }
}
