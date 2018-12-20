using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SaleManagement.Controllers.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleManagement.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult SearchCustomer_JSON(string phoneNumber)
        {
            string converted = JsonConvert.SerializeObject(
                CustomerCRUD.SeachCustomer(this, phoneNumber, null),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }
    }
} 