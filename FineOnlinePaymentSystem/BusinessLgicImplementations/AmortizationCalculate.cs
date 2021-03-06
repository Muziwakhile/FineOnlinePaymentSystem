﻿using FineOnlinePaymentSystem.BusinessLogicInterfaces;
using FineOnlinePaymentSystem.DataOpsInterfaces;
using FineOnlinePaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.BusinessLgicImplementations
{
    public class AmortizationCalculate : IAmortizationCalculate
    {
        private readonly IdataOps<AmortizationSettings> amortizationSettings;
        private int daysOverstayed;
        private readonly int daysbeforeAmortization;
        private readonly int PercentPerday;

        public AmortizationCalculate(IdataOps<AmortizationSettings> _amortizationSettings)
        {
            amortizationSettings = _amortizationSettings;
            daysbeforeAmortization = amortizationSettings.GetById(1).DaysBeforeAmortization;
            PercentPerday = amortizationSettings.GetById(1).PercentPerDay;
        }

        public decimal AmortizationAmount(Case _case, Fine fine)
        {
            int totaldays = ((TimeSpan)(_case.CourtDate - _case.DateOfArrest)).Days;
            daysOverstayed = totaldays - daysbeforeAmortization;
            int percent = PercentPerday * daysOverstayed;

            decimal result =((decimal)percent/100m );

            return (decimal)result * (decimal)fine.Amount;
        }

       
        public int DaysOverstayed(Case _case)
        {
            int totaldays = ((TimeSpan)(_case.CourtDate - _case.DateOfArrest)).Days;

            daysOverstayed = totaldays - daysbeforeAmortization;

            return daysOverstayed;
        }

        public int AmortizationPercent(Case _case)
        {
            int totaldays = ((TimeSpan)(_case.CourtDate - _case.DateOfArrest)).Days;
            daysOverstayed = totaldays - daysbeforeAmortization;

            return PercentPerday * daysOverstayed;
        }

        public decimal AmountPayable(Case _case, Fine fine)
        {
            decimal amortamount = AmortizationAmount(_case, fine);

            return fine.Amount - amortamount;
        }
    }
}
