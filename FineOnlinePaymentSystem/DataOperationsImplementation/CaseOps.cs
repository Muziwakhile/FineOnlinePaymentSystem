using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOpsInterfaces;
using FineOnlinePaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
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


        public List<int> ListByCaseNUmber2(int caseNumber)
        {
            var nums = new List<int>();
            var result = context.Cases.FromSqlRaw($"Select * from Cases where CaseNumber like '%{caseNumber}%'").ToList();

            foreach (var item in result)
            {
                nums.Add(item.CaseNumber);
            }

            return nums;
        }

        public List<Case> SearchByStatus(int status)
        {
            return context.Cases.Where<Case>(c => c.CaseStatusID == status).ToList<Case>();
        }
    }
}
