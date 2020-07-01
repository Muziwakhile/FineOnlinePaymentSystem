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


        public List<string> ListByOffenderPIN(string PIN,int caseNumber)
        {
            var nums = new List<string>();
            var result = context.Offenders.FromSqlRaw($"SELECT * from Offenders where PIN LIKE '%{PIN}%' AND OffenderID IN (SELECT OffenderID FROM CaseOffenders WHERE CaseID = (SELECT CaseID FROM Cases WHERE CaseNumber = {caseNumber}))").ToList();

            foreach (var item in result)
            {
                nums.Add(item.PIN);
            }

            return nums;
        }
    }
}
