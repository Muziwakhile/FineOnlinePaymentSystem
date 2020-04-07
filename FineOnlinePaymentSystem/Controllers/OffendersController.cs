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
    [Authorize(Roles ="SuperAdmin")]
    public class OffendersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly OffenderOps offenderOps;

        public OffendersController(ApplicationDbContext _context)
        {
            context = _context;
            offenderOps = new OffenderOps(context);
        }


        [Route("Offenders")]
        [Route("Offenders/index")]
        //[Route("Offenders/Index/{Id?}")]
        public IActionResult Index(string pin)
        {
            if (pin != null)
            {
                List<Offender> result = new List<Offender>();
                result.Add(offenderOps.SearchByPin(pin));
                return View(result);
            }
          
            return View(offenderOps.GetAll());
        }

        [HttpGet]
        [Route("Offenders/Index/{Id?}")]
        //[ActionName("Index")]
        public IActionResult Index(int id)
        {
            // if the id has been provided then it should get the offender by the ID

            List<Offender> result = new List<Offender>();
            result.Add(offenderOps.GetById(id));
            return View(result );
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Offender offender)
        {
            if (ModelState.IsValid)
            {
                //set the status to be "in Custody"
                offender.StatusID = 1;

                //insert the offender in the database
                offenderOps.Insert(offender);
            }

            return RedirectToAction("Index","Offenders");
        }
    }
}