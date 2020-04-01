using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOpsInterfaces;
using FineOnlinePaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.DataOperationsImplementation
{
    public class RelativeOps : CrudOperations<Relative>, ISearchByPin<Relative>
    {
        private readonly ApplicationDbContext context;

        public RelativeOps(ApplicationDbContext _context):base(_context)
        {
            context = _context;
        }
        public Relative SearchByPin(string pin)
        {
            return context.Relatives.Where<Relative>(r => r.PIN == pin).FirstOrDefault();
        }
    }
}
