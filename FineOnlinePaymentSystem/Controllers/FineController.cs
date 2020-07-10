using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FineOnlinePaymentSystem.BusinessLogicInterfaces;
using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOperationsImplementation;
using FineOnlinePaymentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FineOnlinePaymentSystem.Controllers
{
    public class FineController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICheckAmortization checkAmortization;
        private readonly IAmortizationCalculate amortizationCalculate;
        private readonly CaseOps caseOps;
        private readonly CaseOffenderOps caseOffenders;
        private readonly OffenderOps offenders;
        private readonly CrudOperations<Fine> crudOps;
        private readonly CrudOperations<Amortization> crudOps2;

        public FineController(ApplicationDbContext _context, ICheckAmortization _checkAmortization, IAmortizationCalculate _amortizationCalculate)
        {
            context = _context;
            checkAmortization = _checkAmortization;
            amortizationCalculate = _amortizationCalculate;
            caseOps = new CaseOps(_context);
            caseOffenders = new CaseOffenderOps(_context);
            offenders = new OffenderOps(_context);
            crudOps = new FineOps(_context);
            crudOps2 = new CrudOperations<Amortization>(_context);
        }


        [HttpGet]
        public IActionResult Index(int caseNumber)
        {
            if (caseNumber != 0 && caseNumber > 0 )
            {
                var _case = caseOps.SearchByCaseNumber(caseNumber);
                var fine = crudOps.GetAll().Where(f => f.CaseID == _case.CaseID).ToList<Fine>();

                return View(fine);
            }
            var results = crudOps.GetAll();
            return View(results);
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult CreateFine()
        {
            return View();
        }



        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult CreateFine(Fine fine)
        {

            fine.CaseID = caseOps.SearchByCaseNumber(fine.Case.CaseNumber).CaseID;
            var offender = offenders.SearchByPin(fine.Offender.PIN);
            var _caseOffender = context.CaseOffenders.Where<CaseOffender>(c => c.CaseID == fine.CaseID && c.OffenderID == offender.OffenderID).FirstOrDefault<CaseOffender>();

            if (_caseOffender != null)
            {
                fine.OffenderID = offender.OffenderID;
                fine.FineStatusID = 1;
                var _case = caseOps.GetById(fine.CaseID);

                if (checkAmortization.CheckCaseDates(_case))
                {
                    
                    crudOps.Insert(new Fine { 
                    Amount = fine.Amount,
                    CaseID = _case.CaseID,
                    OffenderID = offender.OffenderID,
                    FineStatusID = fine.FineStatusID
                    });

                    var amortization = new Amortization
                    {
                        FineID = context.Fines.Where<Fine>(c => c.CaseID == _case.CaseID && c.OffenderID == offender.OffenderID).Select(c => c.FineID).FirstOrDefault(),
                        CaseID = _case.CaseID,
                        DaysOverstayed = amortizationCalculate.DaysOverstayed(_case),
                        Percent = amortizationCalculate.AmortizationPercent(_case),
                        AmortizationAmount = amortizationCalculate.AmortizationAmount(_case, fine)
                    };

                    crudOps2.Insert(amortization);
                    ViewBag.Message = "Fine captured successfuly";
                    ViewBag.MessageType = "Success";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Incomplete Case details: Date of arrest or court not captured";
                    ViewBag.MessageType = "Warning";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Offender not found in the Specified Case";
                ViewBag.MessageType = "Warning";
                return View();
            }

        }

        [HttpGet]
        public JsonResult ListByCaseNUmber2(int caseNumber)
        {

            var result = caseOps.ListByCaseNUmber2(caseNumber);

            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return Json(result);
            }

        }


        [HttpGet]
        public JsonResult ListByPIN(string PIN, int caseNumber)
        {

            var result = caseOffenders.ListByOffenderPIN(PIN, caseNumber);

            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return Json(result);
            }

        }
    }
}
