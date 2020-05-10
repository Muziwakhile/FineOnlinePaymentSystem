using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.DataOpsInterfaces
{
    public interface ISearchByCaseID<T>
    {
        List<T> GetByCaseIDOnly(int Id);
    }
}
