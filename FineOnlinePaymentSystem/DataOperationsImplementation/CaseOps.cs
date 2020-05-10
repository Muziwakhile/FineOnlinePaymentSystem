using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOpsInterfaces;
using FineOnlinePaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.DataOperationsImplementation
{
    public class CaseOps : CrudOperations<Case>, ISearchByStatus<Case>,ISearchByCaseNumber<Case>
    {
        private readonly ApplicationDbContext context;

        public CaseOps(ApplicationDbContext _context):base(_context)
        {
            context = _context;
        }

       

        public Case SearchByCaseNumber(int caseNumber)
        {
            return context.Cases.Where<Case>(c => c.CaseNumber == caseNumber).FirstOrDefault();
        }

        public List<Case> SearchByStatus(int status)
        {
            return context.Cases.Where<Case>(c => c.CaseStatusID == status).ToList<Case>();
        }
    }
}
