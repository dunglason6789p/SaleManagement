using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SaleManagement.Controllers.DBCreating;
using SaleManagement.Controllers.Session;

namespace SaleManagement.Controllers
{
    public class DebugController : Controller
    {
        public ActionResult Index()
        {
            Debug.WriteLine("Session[UserName]="+this.GetSession(SessionKey.UserName)+"----------------------------------------");

            return View();
        }

        public ActionResult SessionInit()
        {
            this.SetSession("UserName", "abc");
            this.SetSession("StoreID", 1);

            return View();
        }

        public ActionResult Index1()
        {
            while (true)
            {
                int oldValue = 1000000;
                Debug.WriteLine("oldValue=" + oldValue + " :: newValue=" + DBCreating.ProductHelper_.CostFluctuated(oldValue) + "----------------------------------------------");
            }

            return View();
        }
    }
}