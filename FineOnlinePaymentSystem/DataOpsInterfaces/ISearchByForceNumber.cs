using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.DataOpsInterfaces
{
    interface ISearchByForceNumber<T>
    {
        T SearchByForceNumber(int forceNumber);
    }
}
