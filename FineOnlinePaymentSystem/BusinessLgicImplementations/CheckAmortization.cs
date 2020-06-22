using FineOnlinePaymentSystem.BusinessLogicInterfaces;
using FineOnlinePaymentSystem.DataOperationsImplementation;
using FineOnlinePaymentSystem.DataOpsInterfaces;
using FineOnlinePaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineOnlinePaymentSystem.BusinessLgicImplementations
{
    public class CheckAmortization : ICheckAmortization
    {
        private readonly IdataOps<AmortizationSettings> crudOps;

        public CheckAmortization(IdataOps<AmortizationSettings> crudOperations)
        {
            crudOps = crudOperations;
        }
        public bool CheckCaseDates(Case model)
        {
            if (model.DateOfArrest != null && model.CourtDate != null )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckAmortizationStatus(Case model)
        {
                int datedif = ((TimeSpan)(model.CourtDate - model.DateOfArrest)).Days;
                if (datedif > crudOps.GetById(1).DaysBeforeAmortization)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
    }
}
