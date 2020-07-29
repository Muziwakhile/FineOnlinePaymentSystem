using FineOnlinePaymentSystem.BusinessLogicInterfaces;
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
        private readonly int DaysToBeInJail;

        public AmortizationCalculate(IdataOps<AmortizationSettings> _amortizationSettings)
        {
            amortizationSettings = _amortizationSettings;
            daysbeforeAmortization = amortizationSettings.GetById(1).DaysBeforeAmortization;
            PercentPerday = amortizationSettings.GetById(1).PercentPerDay;
            DaysToBeInJail = amortizationSettings.GetById(1).DaysToBeInJail;
            daysOverstayed = default;
        }

        public decimal AmortizationAmount(Case _case, Fine fine)
        {

            int totaldays = ((TimeSpan)(_case.CourtDate - _case.DateOfArrest)).Days;
            daysOverstayed = totaldays - daysbeforeAmortization;

            int percent = PercentPerday * daysOverstayed;

            decimal result = ((decimal)percent / 100m);

            return (decimal)result * (decimal)fine.Amount;

        }



        public decimal AmortizationAmountNewFormula(Case _case, Fine _fine)
        {

            int totaldays = ((TimeSpan)(_case.CourtDate - _case.DateOfArrest)).Days;
            daysOverstayed = totaldays - daysbeforeAmortization;

            if (daysOverstayed >= 0)
            {
                int _daysinJail = DaysInJail(_case, _fine);

                int totalDaysInCustody = daysOverstayed + _daysinJail;
                decimal amortization = totalDaysInCustody * AmortizationAmountPerDay(_fine);

                return amortization;
            }
            else
            {
                int _daysinJail = DaysInJail(_case, _fine);

                int totalDaysInCustody = daysOverstayed + _daysinJail;
                decimal amortization = totalDaysInCustody * AmortizationAmountPerDay(_fine);

                return amortization;
            }
            

        }


        public int DaysOverstayed(Case _case)
        {
            int totaldays = ((TimeSpan)(_case.CourtDate - _case.DateOfArrest)).Days;
            if (totaldays >= 0 )
            {
                daysOverstayed = totaldays - daysbeforeAmortization;

                return daysOverstayed;
            }
            else
            {
                return daysOverstayed;
            }
          
        }

        public int DaysInJailRemaining(Case _case,Fine _fine)
        {
            int _daysOverstayed = DaysOverstayed(_case);
            int totaldays = ((TimeSpan)(_fine.ReleaseDate - DateTime.Now)).Days - _daysOverstayed;

            //daysOverstayed = totaldays - daysbeforeAmortization;

            return totaldays;
        }


        public int DaysInJail(Case _case, Fine _fine)
        {
            
            int totaldays = ((TimeSpan)(DateTime.Now - _case.CourtDate)).Days;

            //daysOverstayed = totaldays - daysbeforeAmortization;

            return totaldays;
        }


        public DateTime ReleaseDate(Case _case)
        {
            int _daysOverstayed = DaysOverstayed(_case);
            int daystostay = DaysToBeInJail -_daysOverstayed;
            DateTime date = (DateTime)_case.CourtDate;

            var d = date.AddDays(daystostay);
            //daysOverstayed = totaldays - daysbeforeAmortization;

            return d;
        }


        public int AmortizationPercent(Case _case)
        {
            int totaldays = ((TimeSpan)(_case.CourtDate - _case.DateOfArrest)).Days;
            daysOverstayed = totaldays - daysbeforeAmortization;

            return PercentPerday * daysOverstayed;
        }

        public decimal AmortizationAmountPerDay(Fine _fine)
        {
            decimal amount = _fine.Amount / DaysToBeInJail;

            return amount;
        }

        public decimal AmountPayable(Case _case, Fine fine)
        {
            decimal amortamount = AmortizationAmount(_case, fine);

            return fine.Amount - amortamount;
        }
    }
}
