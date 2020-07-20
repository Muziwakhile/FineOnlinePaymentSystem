using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOperationsImplementation;
using FineOnlinePaymentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FineOnlinePaymentSystem.Controllers
{
    public class OfficerController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly OfficerOps officer;

        public OfficerController(ApplicationDbContext _context)
        {
            context = _context;
            officer = new OfficerOps(context);

        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index(int forceNumber)
        {

            var r = new List<Officer>();
            if (forceNumber != 0)
            {
                var result = officer.SearchByForceNumber(forceNumber);

                if (result != null)
                {
                    r.Add(result);
                    return View(r);
                }
                else
                {
                    ViewBag.Message = "Force number not found in the System";
                    ViewBag.MessageType = "Warining";
                    return View(r);
                }
            }
            else
            {
                return View(officer.GetAll());
            }
           
            
        }




        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Edit(int id)
        {
            var result = officer.GetById(id);
            return View(result);
        }


        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Edit(Officer _officer)
        {
            var result = officer.GetById(_officer.OfficerID);

            result.Name = _officer.Name;
            result.Surname = _officer.Surname;
            result.ForceNumber = _officer.ForceNumber;
            result.Contact = _officer.Contact;

            officer.Update(result);

           return RedirectToAction("index");
        }



        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Details(int id)
        {
            var results = officer.GetById(id);

            return View(results);
        }



        [HttpGet]
        public JsonResult SearchByForceNumber(int forceNumber)
        {

            var result = officer.ListByForceNumber(forceNumber);

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
