using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOpsInterfaces;
using FineOnlinePaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.DataOperationsImplementation
{
    public class OffenderOps : CrudOperations<Offender>, ISearchByPin<Offender>,ISearchByStatus<Offender>
    {
        private readonly ApplicationDbContext context;

        public OffenderOps(ApplicationDbContext _context):base(_context)
        {
            context = _context;
        }
        public Offender SearchByPin(string pin)
        {
            return context.Offenders.Where<Offender>(of => of.PIN == pin).FirstOrDefault();
        }

        public List<Offender> SearchByStatus(int status)
        {
            return context.Offenders.Where<Offender>(of => of.StatusID == status).ToList<Offender>();
        }
    }
}
