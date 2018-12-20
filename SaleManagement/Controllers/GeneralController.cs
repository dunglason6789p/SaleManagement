using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleManagement.Controllers
{
    public class GeneralController : Controller
    {
        // GET: General
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View("~/Views/admin_1_rounded/page_general_about.cshtml");
        }

        public ActionResult Contact()
        {
            return View("~/Views/admin_1_rounded/page_general_contact.cshtml");
        }
        public ActionResult Pricing()
        {
            return View("~/Views/admin_1_rounded/page_general_pricing.cshtml");
        }
    }
}