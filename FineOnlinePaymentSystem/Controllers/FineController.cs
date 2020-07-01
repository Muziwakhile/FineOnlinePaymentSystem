using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOperationsImplementation;
using Microsoft.AspNetCore.Mvc;

namespace FineOnlinePaymentSystem.Controllers
{
    public class FineController : Controller
    {
        private readonly CaseOps caseOps;
        private readonly CaseOffenderOps caseOffenders;

        public FineController(ApplicationDbContext _context)
        {
            caseOps = new CaseOps(_context);
            caseOffenders = new CaseOffenderOps(_context);

        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CreateFine()
        {
            return View();
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
        public JsonResult ListByPIN(string PIN,int caseNumber)
        {

            var result = caseOffenders.ListByOffenderPIN(PIN,caseNumber);

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
