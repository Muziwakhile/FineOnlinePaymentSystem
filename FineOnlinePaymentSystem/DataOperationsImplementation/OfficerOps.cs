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
    public class OfficerOps : CrudOperations<Officer>, ISearchByForceNumber<Officer>
    {
        private readonly ApplicationDbContext context;

        public OfficerOps(ApplicationDbContext _context):base(_context)
        {
            context = _context;
        }

        public List<int> ListByForceNumber(int ForceNumber)
        {
            var nums = new List<int>();
            var result = context.Officers.FromSqlRaw($"Select * from Officers where ForceNumber like '%{ForceNumber}%'").ToList();

            foreach (var item in result)
            {
                nums.Add(item.ForceNumber);
            }

            return nums;
        }

        public Officer SearchByForceNumber(int forceNumber)
        {
            return context.Officers.Where<Officer>(o => o.ForceNumber == forceNumber).FirstOrDefault();
        }
    }
}
