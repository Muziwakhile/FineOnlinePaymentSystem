using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOpsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FineOnlinePaymentSystem.DataOperationsImplementation
{
    public class CrudOperations<T> : IdataOps<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;
        public CrudOperations(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public virtual void Delete(T model)
        {
            dbContext.Remove<T>(model);
            dbContext.SaveChanges();
        }

        public virtual List<T> GetAll()
        {
            return dbContext.Set<T>().ToList<T>();
        }

        public virtual T GetById(int Id)
        {
            return dbContext.Find<T>(Id);
        }

        public virtual void Insert(T model)
        {
            dbContext.Add<T>(model);
            dbContext.SaveChanges();
        }

        public virtual void Update(T model)
        {
            
            dbContext.Update<T>(model);
            dbContext.SaveChanges();
        }
    }
}
