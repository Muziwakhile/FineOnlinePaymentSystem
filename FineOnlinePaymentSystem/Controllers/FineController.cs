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
using Microsoft.AspNetCore.Mvc.Rendering;

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
        private readonly CrudOperations<Fine> fineOps;
        private readonly CrudOperations<FineStatus> status;

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
            fineOps = new CrudOperations<Fine>(context);
            status = new CrudOperations<FineStatus>(context);
        }


        [HttpGet]
        [Authorize(Roles ="SuperAdmin,Officer")]
        public IActionResult Index(int caseNumber,int status)
        {
            ViewBag.Status = new SelectList(this.status.GetAll(), "FineStatusID", "Name");

            if (caseNumber != 0 && caseNumber > 0)
            {
                var _case = caseOps.SearchByCaseNumber(caseNumber);
                var fine = fineOps.GetAll().Where(f => f.CaseID == _case.CaseID).ToList<Fine>();

                return View(fine);
            }
            else if (status > 0)
            {
                var results = fineOps.GetAll().Where( s => s.FineStatusID == status);
                return View(results);
            }
            else
            {
                var results = fineOps.GetAll();
                return View(results);
            }
           
        }


        [HttpGet]
        [Authorize(Roles = "Officer")]
        public IActionResult CreateFine()
        {
            return View();
        }



        [HttpPost]
        [Authorize(Roles = "Officer")]
        public IActionResult CreateFine(Fine fine)
        {

            fine.CaseID = caseOps.SearchByCaseNumber(fine.Case.CaseNumber).CaseID;
            var offender = offenders.SearchByPin(fine.Offender.PIN);

            if (offender != null)
            {
                var _caseOffender = context.CaseOffenders.Where<CaseOffender>(c => c.CaseID == fine.CaseID && c.OffenderID == offender.OffenderID).FirstOrDefault<CaseOffender>();

                if (_caseOffender != null)
                {
                    fine.OffenderID = offender.OffenderID;
                    fine.FineStatusID = 1;
                    var _case = caseOps.GetById(fine.CaseID);

                    if (checkAmortization.CheckCaseDates(_case))
                    {
                        if (checkAmortization.CheckAmortizationStatus(_case))
                        {
                            crudOps.Insert(new Fine
                            {
                                Amount = fine.Amount,
                                ReleaseDate = amortizationCalculate.ReleaseDate(_case),
                                CaseID = _case.CaseID,
                                OffenderID = offender.OffenderID,
                                FineStatusID = fine.FineStatusID
                            });


                            //var _amortization = new Amortization
                            //{
                            //    FineID = context.Fines.Where<Fine>(c => c.CaseID == _case.CaseID && c.OffenderID == offender.OffenderID).Select(c => c.FineID).FirstOrDefault(),
                            //    CaseID = _case.CaseID,
                            //    DaysOverstayed = amortizationCalculate.DaysOverstayed(_case),
                            //    Percent = amortizationCalculate.AmortizationPercent(_case),
                            //    AmortizationAmount = amortizationCalculate.AmortizationAmount(_case, fine)
                            //};

                            //crudOps2.Insert(_amortization);


                            ViewBag.Message = "Fine captured successfuly";
                            ViewBag.MessageType = "Success";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            crudOps.Insert(new Fine
                            {
                                Amount = fine.Amount,
                                ReleaseDate = amortizationCalculate.ReleaseDate(_case),
                                CaseID = _case.CaseID,
                                OffenderID = offender.OffenderID,
                                FineStatusID = fine.FineStatusID
                            });


                            //var _amortization2 = new Amortization
                            //{
                            //    FineID = context.Fines.Where<Fine>(c => c.CaseID == _case.CaseID && c.OffenderID == offender.OffenderID).Select(c => c.FineID).FirstOrDefault(),
                            //    CaseID = _case.CaseID,
                            //    DaysOverstayed = default,
                            //    Percent = default,
                            //    AmortizationAmount = default
                            //};

                            //crudOps2.Insert(_amortization2);
                            return RedirectToAction("Index");
                        }

                    }
                    else
                    {
                        ViewBag.Message = "Incomplete Case details: Date of arrest or court date was not captured";
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
            else
            {
                ViewBag.Message = "Offender does not exist in our database";
                ViewBag.MessageType = "Warning";
                return View();
            }


        }




        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Edit(int id)
        {
            
            var result = fineOps.GetById(id);

            if (result.FineStatusID == 2)
            {
                return RedirectToAction("Details", new { Id = id});
            }
            else
            {
                return View(result);
            }
           
        }


        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Edit(Fine _fine)
        {
            var _case = caseOps.GetById(_fine.CaseID);

            //if (checkAmortization.CheckAmortizationStatus(_case))
            //{
            //    var _amortizationUpdate = crudOps2.GetAll().Where(am => am.CaseID == _case.CaseID && am.FineID == _fine.FineID).SingleOrDefault<Amortization>();

            //    _amortizationUpdate.DaysOverstayed = amortizationCalculate.DaysOverstayed(_case);
            //    _amortizationUpdate.Percent = amortizationCalculate.AmortizationPercent(_case);
            //    _amortizationUpdate.AmortizationAmount = amortizationCalculate.AmortizationAmount(_case, _fine);

            //    crudOps2.Update(_amortizationUpdate);
            //    return RedirectToAction("index");
            //}
            //else
            //{
            //    var fineUpdate = crudOps.GetById(_fine.FineID);
            //    fineUpdate.Amount = _fine.Amount;

            //    crudOps.Update(fineUpdate);

            //    var amortizationUpdate = crudOps2.GetAll().Where(am => am.CaseID == _case.CaseID && am.FineID == _fine.FineID).SingleOrDefault<Amortization>();

            //    amortizationUpdate.DaysOverstayed = amortizationCalculate.DaysOverstayed(_case);
            //    amortizationUpdate.Percent = amortizationCalculate.AmortizationPercent(_case);
            //    amortizationUpdate.AmortizationAmount = amortizationCalculate.AmortizationAmount(_case, _fine);

            //    crudOps2.Update(amortizationUpdate);

            //    return RedirectToAction("index");
            //}


            var fineUpdate = crudOps.GetById(_fine.FineID);
            fineUpdate.Amount = _fine.Amount;

            crudOps.Update(fineUpdate);
            return RedirectToAction("index");


        }


        [HttpGet]
        [Authorize(Roles ="SuperAdmin,Officer")]
        public IActionResult Details(int Id)
        {
            var result = fineOps.GetById(Id);
            return View(result);
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
