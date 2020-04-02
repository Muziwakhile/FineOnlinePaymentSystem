using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FineOnlinePaymentSystem.Data;
using FineOnlinePaymentSystem.DataOperationsImplementation;
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

        public IActionResult Index()
        {
            return View(offenderOps.GetAll());
        }
    }
}