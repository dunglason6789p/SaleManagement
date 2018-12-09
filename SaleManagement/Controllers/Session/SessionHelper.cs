using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleManagement.Controllers.Session
{
    public static class SessionHelper
    {
        public static String GetSession(this Controller controller, string key)
        {
            if (controller.Session[key] != null)
                return controller.Session[key].ToString();
            else return "0"; // <-- FOR DEBUG ONLY !

            /* THIS IS GOOD !. BUT WE ARE NOT IN PRODUCTION MODE YET.
            if (controller.Session[key] != null) return controller.Session[key].ToString();
            else return null;
            */
        }

        public static void SetSession(this Controller controller, string key, string value)
        {
            controller.Session[key] = value;
        }

        public static void SetSession(this Controller controller, string key, int value)
        {
            controller.Session[key] = value;
        }

        public static void SetSession(this Controller controller, string key, object value)
        {
            controller.Session[key] = value;
        }
    }
}