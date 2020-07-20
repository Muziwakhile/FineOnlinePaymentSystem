using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOperationsImplementation;
using FineOnlinePaymentSystem.Models;
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
