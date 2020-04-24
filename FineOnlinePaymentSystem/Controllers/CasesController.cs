using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOperationsImplementation;
using FineOnlinePaymentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FineOnlinePaymentSystem.Controllers
{
    public class CasesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly CaseOps caseOps;
        private readonly OffenderOps offender;
        private readonly CrudOperations<Offence> offence;
        private readonly OfficerOps officer;
        private readonly CrudOperations<CaseOffender> caseof;

        public CasesController(ApplicationDbContext _context)
        {
            context = _context;
            caseOps = new CaseOps(context);
            offender = new OffenderOps(context);
            offence = new CrudOperations<Offence>(context);
            officer = new OfficerOps(context);
            caseof = new CrudOperations<CaseOffender>(context);
        }


        public IActionResult Index(int status,int caseNumber)
        {
            var c = new List<Case>();
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



        public IActionResult Create()
        {
            ViewBag.OffenceID = new SelectList(offence.GetAll(), "OffenseID", "Name");
            ViewBag.OfficerID = new SelectList(officer.GetAll(), "OfficerID", "ForceNumber");
            return View();
        }

        [HttpPost]
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
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(Case model)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SearchByPin(string pin)
        {

            var caseID = (int)TempData["CaseID"];
            var result = offender.SearchByPin(pin);
            if (result != null)
            {
                caseof.Insert(new CaseOffender { CaseID = caseID, OffenderID = result.OffenderID });
                return Json( new { ID = caseID});
            }
            else
            {
                return Json(new { ID = caseID });
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
    }
}