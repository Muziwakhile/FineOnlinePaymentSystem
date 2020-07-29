using FineOnlinePaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.BusinessLogicInterfaces
{
    public interface IAmortizationCalculate
    {
        decimal AmortizationAmount(Case _case,Fine fine);
        int DaysOverstayed(Case _case);
        int AmortizationPercent(Case _case);
        decimal AmountPayable(Case _case, Fine fine);

        int DaysInJail(Case _case, Fine _fine);
        int DaysInJailRemaining(Case _case, Fine _fine);
        DateTime ReleaseDate(Case _case, Fine _fine);
        decimal AmortizationAmountPerDay( Fine _fine);
        decimal AmortizationAmountNewFormula(Case _case, Fine fine);
    }
}
