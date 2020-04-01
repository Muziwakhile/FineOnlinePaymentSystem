using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.DataOpsInterfaces
{
    public interface IdataOps<T>
    {
        List<T> GetAll();
        T GetById(int Id);
        void Insert(T model);
        void Update(T model);
        void Delete(T model);

    }
}
