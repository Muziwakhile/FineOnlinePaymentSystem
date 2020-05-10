using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOpsInterfaces;
using FineOnlinePaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.DataOperationsImplementation
{
    public class CaseOffenderOps : CrudOperations<CaseOffender>, ISearchByCaseID<CaseOffender>
    {
        private readonly ApplicationDbContext context;

        public CaseOffenderOps(ApplicationDbContext _context) : base(_context)
        {
            context = _context;
        }
        public List<CaseOffender> GetByCaseIDOnly(int Id)
        {
            return context.CaseOffenders.Where(cof => cof.CaseID == Id).ToList<CaseOffender>();
        }
    }
}
