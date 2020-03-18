using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.Models
{
    public class Case
    {
        public int CaseID { get; set; }
        public int CaseNumber { get; set; }
        public string CaseDescription { get; set; }
        public string CrimeLocation { get; set; }
        public DateTime DateOfCrime { get; set; }
        public DateTime? DateOfArrest { get; set; }
        public DateTime? CourtDate { get; set; }

        public int OfficerID { get; set; }
        public Officer Officer { get; set; }


        public ICollection<CaseOffenders> CaseOffenders { get; set; }

    }
}
