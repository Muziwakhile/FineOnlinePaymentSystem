using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOperationsImplementation;
using FineOnlinePaymentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FineOnlinePaymentSystem.Controllers
{
    public class CasesController : Controller
    {

        //private varables that will be used to access differe CRUD operations
        private readonly ApplicationDbContext context;
        private readonly CaseOps caseOps;
        private readonly OffenderOps offender;
        private readonly CrudOperations<Offence> offence;
        private readonly OfficerOps officer;
        private readonly CaseOffenderOps caseof;


        //Contractor that loads initialies all the private fileds above
        public CasesController(ApplicationDbContext _context)
        {
            context = _context;
            caseOps = new CaseOps(context);
            offender = new OffenderOps(context);
            offence = new CrudOperations<Offence>(context);
            officer = new OfficerOps(context);
            caseof = new CaseOffenderOps(context);
        }


        //these are 2 routes that will redirect to this action
        [Route("Cases")]
        [Route("Cases/index")]
        [HttpGet]
        //Action method that will be used to display all the cases
        //this action will also be used to filter searches according to either the status or the case number
        public IActionResult Index(int status,int caseNumber)
        {

            ViewBag.Status = new SelectList(context.CaseStatuses.ToList<CaseStatus>(), "CaseStatusID", "Name");
            var c = new List<Case>();

            //check if the status argument is given so that the list can be filtered by the status
            //Else if the case number arfument so that the list can filter using the casenumber
            if (status > 0)
            {
                return View(caseOps.SearchByStatus(status));
            }
            else if (caseNumber > 0)
            {
                c.Add(caseOps.SearchByCaseNumber(caseNumber));
                return View(c);
            }
            return View(caseOps.GetAll());
        }

        [Route("Cases/Create")]
        [HttpGet]
        [Authorize(Roles ="Officer")]
        public IActionResult Create()
        {
            ViewBag.OffenceID = new SelectList(offence.GetAll(), "OffenseID", "Name");
            ViewBag.OfficerID = new SelectList(officer.GetAll(), "OfficerID", "ForceNumber");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Officer")]
        public IActionResult Create(Case model)
        {
            
            model.CaseStatusID = 1;
            model.Officer = officer.SearchByForceNumber(model.Officer.ForceNumber);
            caseOps.Insert(model);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int ID)
        {
            TempData["CaseID"] = ID;
            var result = caseOps.GetById(ID);
            ViewBag.OffenceID = new SelectList(offence.GetAll(), "OffenseID", "Name");
            //ViewBag.caseoff = caseof.GetById(ID);
            return View(result);
        }



        [HttpPost]
        public IActionResult Edit(Case model)
        {

            var ofresult = officer.SearchByForceNumber(model.Officer.ForceNumber);
            model.OfficerID = ofresult.OfficerID;

            if ( model.OffenceID > 0 || model.OfficerID > 0)
            {

                var fromdb = caseOps.GetById(model.CaseID);
                

                fromdb.CaseDescription = model.CaseDescription;
                fromdb.CrimeLocation = model.CrimeLocation;
                fromdb.DateOfArrest = model.DateOfArrest;
                fromdb.DateOfCrime = model.DateOfCrime;
                fromdb.OffenceID = model.OffenceID;
                fromdb.OfficerID = model.OfficerID;
                fromdb.CourtDate = model.CourtDate;
                caseOps.Update(fromdb);
            }
           
            return RedirectToAction("Index");
        }




        [HttpGet]
        public IActionResult Details(int ID)
        {
            var result = caseOps.GetById(ID);
            return View(result);
        }



        [HttpGet]
        [Authorize(Roles ="SuperAdmin")]
        public IActionResult Delete(int caseID, int offenderID)
        {
            //var caseID = (int)ViewBag.CaseID;
            caseof.Delete(new CaseOffender { CaseID = caseID, OffenderID = offenderID });
            //var model = caseOps.GetById(caseID);

            return  RedirectToAction("Edit",new { ID = caseID });
        }




        [HttpGet]
        public IActionResult SearchByPin(string pin,int caseId)
        {

          
            var result = offender.SearchByPin(pin);
            if (result != null)
            {
                caseof.Insert(new CaseOffender { CaseID = caseId, OffenderID = result.OffenderID });
                return Json( new { ID = caseId });
            }
            else
            {
                return Json(new { ID = caseId });
            }
           
        }


        //Action for adding offender while creating a case(This feature is not complete)
        //[HttpGet]
        //public IActionResult SearchByPinCreate(string pin)
        //{
        //    List<Offender> offenders = new List<Offender>();
        //    var result = offender.SearchByPin(pin);
        //    if (result != null)
        //    {
        //        offenders.Add(result);
        //        return PartialView("_SearchByPinPartial", offenders);
        //    }
        //    else
        //    {
        //        return Json(result);
        //    }
        //}



        [HttpGet]
        public IActionResult LoadCOF(int ID)
        {
            var cofmodel= caseof.GetByCaseIDOnly(ID);

            
            if (cofmodel != null)
            {
                return PartialView("_SearchPartial", cofmodel);
            }
            else
            {
                return null;
            }
           
        }



        [HttpGet]
        public IActionResult LoadCOF2(int ID)
        {
            var cofmodel = caseof.GetByCaseIDOnly(ID);


            if (cofmodel != null)
            {
                return PartialView("_SearchPartial2", cofmodel);
            }
            else
            {
                return null;
            }

        }




        [HttpGet]
        public JsonResult SearchByForceNumber(int forceNumber)
        {

            var result = officer.ListByForceNumber(forceNumber);

            if (result!=null)
            {
                return Json( result);
            }
            else
            {
                return Json( result);
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
    }
}