using FineOnlinePaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.BusinessLogicInterfaces
{
    public interface ICheckAmortization
    {
        bool CheckCaseDates(Case model);
        bool CheckAmortizationStatus(Case model);

    }
}
