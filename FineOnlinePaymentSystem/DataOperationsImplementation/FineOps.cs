using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.DataOperationsImplementation
{
    public class FineOps:CrudOperations<Fine>
    {
        private readonly ApplicationDbContext context;

        public FineOps(ApplicationDbContext _context):base(_context)
        {
            context = _context;
        }
        

        public override List<Fine> GetAll()
        {
            return base.GetAll().Where<Fine>(c => c.FineStatus.FineStatusID == 1).ToList<Fine>();
        }
    }
}
