using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FineOnlinePaymentSystem.BusinessLgicImplementations;
using FineOnlinePaymentSystem.BusinessLogicInterfaces;
using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOperationsImplementation;
using FineOnlinePaymentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FineOnlinePaymentSystem.Controllers
{
    public class FinePaymentController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly FineOps fineOps;
        private readonly CaseOps caseOps;
        private readonly IAmortizationCalculate amortizationCalculate;
        private readonly CrudOperations<Amortization> amortization;

        public FinePaymentController(ApplicationDbContext _context, IAmortizationCalculate _amortizationCalculate)
        {
            context = _context;
            fineOps = new FineOps(context);
            caseOps = new CaseOps(context);
            amortizationCalculate = _amortizationCalculate;
            amortization = new CrudOperations<Amortization>(context);

        }

        [HttpGet]
        [Authorize(Roles ="Relative")]
        public IActionResult Index(string pin, int caseNumber)
        {
            List<FinePayment> _finePay = new List<FinePayment>();
            if (pin != null && caseNumber != 0)
            {
                var _case = caseOps.SearchByCaseNumber(caseNumber);
                if (_case != null)
                {
                    var _fine = fineOps.GetAll().Where(f => f.Offender.PIN == pin).SingleOrDefault<Fine>();
                    if (_fine != null)
                    {
                        var caseOffender = context.CaseOffenders.Where<CaseOffender>(co => co.CaseID == _case.CaseID && co.OffenderID == _fine.OffenderID).SingleOrDefault<CaseOffender>();
                        if (caseOffender != null)
                        {
                            var userid = context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                            var _amortization = amortization.GetAll().Where(am => am.CaseID == _case.CaseID && am.FineID == _fine.FineID).SingleOrDefault<Amortization>();
                            var fnp = new FinePayment
                            {
                                RelativeID = context.Relatives.Where(r => r.IdentityUserID == userid.Id).FirstOrDefault<Relative>().RelativeID,
                                FineID = _fine.FineID,
                                AmortizationID = _amortization.AmortizationID,
                                AmortizationAmount = _amortization.AmortizationAmount,
                                AmountPayable = amortizationCalculate.AmountPayable(_case, _fine),
                                Fine = _fine,
                                Amortization = _amortization,
                                Relative = context.Relatives.Where(r => r.IdentityUserID == userid.Id).FirstOrDefault<Relative>()
                            };
                            _finePay.Add(fnp);




                            return View(_finePay);
                        }
                        else
                        {
                            ViewBag.Message = "Specified Offender was not found in the specified case";
                            ViewBag.MessageType = "Warining";

                            return View(_finePay);
                        }

                    }
                    else
                    {
                        ViewBag.Message = "Please enter a valid Offender Pin: Pin not found";
                        ViewBag.MessageType = "Warining";

                        return View(_finePay);
                    }
                }
                else
                {
                    ViewBag.Message = "Please enter a valid Case Number: Case number was not found";
                    ViewBag.MessageType = "Warining";

                    return View(_finePay);
                }

                //return View();
            }
            else
            {

                return View(_finePay);
            }

        }



        [HttpGet]
        [Authorize(Roles ="Relative")]
        public IActionResult Edit(int FindeId, int CaseId)
        {
            var _case = caseOps.GetById(CaseId);
            var _fine = fineOps.GetById(FindeId);


            var userid = context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var _amortization = amortization.GetAll().Where(am => am.CaseID == _case.CaseID && am.FineID == _fine.FineID).SingleOrDefault<Amortization>();
            var fnp = new FinePayment
            {
                RelativeID = context.Relatives.Where(r => r.IdentityUserID == userid.Id).FirstOrDefault<Relative>().RelativeID,
                FineID = _fine.FineID,
                AmortizationID = _amortization.AmortizationID,
                AmortizationAmount = _amortization.AmortizationAmount,
                AmountPayable = amortizationCalculate.AmountPayable(_case, _fine),
                Fine = _fine,
                Amortization = _amortization,
                Relative = context.Relatives.Where(r => r.IdentityUserID == userid.Id).FirstOrDefault<Relative>()
            };

            return View(fnp);
        }
    }
}
