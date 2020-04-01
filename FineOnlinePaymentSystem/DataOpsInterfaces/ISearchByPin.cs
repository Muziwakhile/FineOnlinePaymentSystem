using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.DataOpsInterfaces
{
    interface ISearchByPin<T>
    {
        T SearchByPin(string pin);
    }
}
