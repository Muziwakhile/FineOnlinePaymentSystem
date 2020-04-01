using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.DataOpsInterfaces
{
    interface ISearchByCaseNumber<T>
    {
        T SearchByCaseNumber(int caseNumber);
    }
}
