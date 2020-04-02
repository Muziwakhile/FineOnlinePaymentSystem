using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.DataOpsInterfaces
{
    interface ISearchByStatus<T>
    {
        List<T> SearchByStatus(int status);
    }
}
