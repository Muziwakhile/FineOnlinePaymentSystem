using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOpsInterfaces;
using FineOnlinePaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.DataOperationsImplementation
{
    public class OfficerOps : CrudOperations<Officer>, ISearchByForceNumber<Officer>
    {
        private readonly ApplicationDbContext context;

        public OfficerOps(ApplicationDbContext _context):base(_context)
        {
            context = _context;
        }
        public Officer SearchByForceNumber(int forceNumber)
        {
            return context.Officers.Where<Officer>(o => o.FroceNumber == forceNumber).FirstOrDefault();
        }
    }
}
