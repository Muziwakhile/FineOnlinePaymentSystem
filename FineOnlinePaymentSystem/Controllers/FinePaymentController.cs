using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FineOnlinePaymentSystem.BusinessLgicImplementations;
using FineOnlinePaymentSystem.BusinessLogicInterfaces;
using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOperationsImplementation;
using FineOnlinePaymentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace FineOnlinePaymentSystem.Controllers
{
    public class FinePaymentController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly FineOps fineOps;
        private readonly CaseOps caseOps;
        private readonly IAmortizationCalculate amortizationCalculate;
        private readonly CrudOperations<Amortization> amortization;
        private readonly CrudOperations<FinePayment> finepay;
        private readonly OffenderOps offenderOps;
        private static byte[] main;
        public FinePaymentController(ApplicationDbContext _context, IAmortizationCalculate _amortizationCalculate)
        {
            context = _context;
            fineOps = new FineOps(context);
            caseOps = new CaseOps(context);
            amortizationCalculate = _amortizationCalculate;
            amortization = new CrudOperations<Amortization>(context);
            finepay = new CrudOperations<FinePayment>(context);
            offenderOps = new OffenderOps(context);

        }

        [HttpGet]
        [Authorize(Roles = "Relative")]
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
                        ViewBag.Message = "Please enter a valid Offender Pin: Pin not found Or This Fine you're searching has been Paid";
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
                ViewBag.Message = "Please fill in the Case Number and the offender's PIN ";
                ViewBag.MessageType = "Warining";
                return View(_finePay);
            }

        }



        [HttpGet]
        [Authorize(Roles = "Relative")]
        public IActionResult Edit(int FindeId, int CaseId)
        {
            var _case = caseOps.GetById(CaseId);
            var _fine = fineOps.GetById(FindeId);


            var result = finepay.GetAll().Where(f => f.FineID == _fine.FineID).SingleOrDefault<FinePayment>();
            if (result != null && result.FinePaymentStatusID == 2)
            {
                ViewBag.Message = "Fine Has been already payed";
                ViewBag.MessageType = "Warining";
                return View();
            }
            else if (result != null && result.FinePaymentStatusID == 1)
            {
                return View(result);
            }
            else
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

                return View(fnp);
            }

           
        }


        [HttpPost]
        [Authorize(Roles = "Relative")]
        public IActionResult Edit(FinePayment _finePayment, IFormFile Attachment)
        {
            if (Attachment != null)
            {
                using (var stream = new MemoryStream())
                {
                    Attachment.CopyTo(stream);
                    _finePayment.Attachment = stream.ToArray();

                }

                _finePayment.FinePaymentDate = DateTime.Now;
                _finePayment.FinePaymentStatusID = 1;

                finepay.Insert(new FinePayment
                {
                    AmortizationAmount = _finePayment.AmortizationAmount,
                    AmountPayable = _finePayment.AmountPayable,
                    Attachment = _finePayment.Attachment,
                    AmortizationID = _finePayment.AmortizationID,
                    RelativeID = _finePayment.RelativeID,
                    FineID = _finePayment.FineID,
                    FinePaymentStatusID = _finePayment.FinePaymentStatusID,
                    FinePaymentDate = _finePayment.FinePaymentDate
                });

                return RedirectToAction("index");
            }
            else if(_finePayment.Attachment != null)
            {
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.Message = "Please attach prove of payment";
                ViewBag.MessageType = "Warining";
                return View();
            }
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Officer")]
        public IActionResult ViewFinePayment(string pin, int caseNumber)
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

                var _finePayments = finepay.GetAll();
                return View(_finePayments);
            }


        }

        [HttpGet]
        [Authorize(Roles = "Officer")]
        public IActionResult ApprovePayment(int Id)
        {
            var _finepay = finepay.GetById(Id);
            return View(_finepay);
        }


        [HttpPost]
        [Authorize(Roles = "Officer")]
        public IActionResult ApprovePayment(FinePayment _fine)
        {
            var _fineP = finepay.GetById(_fine.FinePaymentID);

            if (_fineP.FinePaymentStatusID != 2)
            {
                var _fi = fineOps.GetById(_fineP.FineID);
                var _case = caseOps.GetById(_fi.CaseID);
                var _offender = offenderOps.GetById(_fi.OffenderID);


                _fineP.FinePaymentStatusID = 2;
                _fi.FineStatusID = 2;
                _case.CaseStatusID = 2;
                _offender.StatusID = 2;


                finepay.Update(_fineP);
                fineOps.Update(_fi);
                caseOps.Update(_case);
                offenderOps.Update(_offender);


                ViewBag.Message = "Fine Payment Approved successfuly";
                ViewBag.MessageType = "Warining";
                return RedirectToAction("ViewFinePayment");
            }
            else
            {
                ViewBag.Message = "Fine Payment Already Approved";
                ViewBag.MessageType = "Warining";
                return RedirectToAction("ViewFinePayment");
            }

        }


        public ActionResult RetrieveImage(int id)
        {
            var result = finepay.GetById(id);

            return new FileContentResult(result.Attachment, "application/pdf");
        }



        public void RetrieveImage2()
        {

            var image = Request.Form.Files[0];

            using var stream = new MemoryStream();
            image.CopyTo(stream);
            main = stream.ToArray();
            //BinaryReader reader = new BinaryReader(image.InputStream);
            //byte[] img = reader.ReadBytes(image.ContentLength);

            //var g = File(img, "image/jpeg");
            //main = img;
            //return Json(new { image = g},JsonRequestBehavior.AllowGet); 


            //return null;
        }

        public ActionResult RetrieveImage3()
        {
            return new FileContentResult(main, "application/pdf");
        }
    }
}
