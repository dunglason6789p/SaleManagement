using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SaleManagement.Controllers.Session;
using SaleManagement.Models;
using SaleManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleManagement.Controllers
{
    public class StaffController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult SearchStaff_JSON(string code, string name, string phoneNumber, string email)
        {
            string converted = JsonConvert.SerializeObject(
                CRUD.StaffCRUD.SearchStaff(this, code, name, phoneNumber, email),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }
        public ActionResult CreateOrEditStaff(int? staffID)
        {
            if (staffID == null)
            {
                return View("~/Views/admin_1_rounded/staff.cshtml", new Staff() { StoreID = Int32.Parse(Session[SessionKey.StoreID].ToString()) });
            }
            Staff staff = _context.Staff.SingleOrDefault(m => m.ID == staffID);
            if (staff == null)
            {
                ViewBag.ErrorString = "Trang mà bạn muốn truy cập không tồn tại !";
                return View("~/Views/admin_1_rounded/page_error.cshtml");
            }
            else
            {
                return View("~/Views/admin_1_rounded/staff.cshtml",staff);
            }
        }
        public ActionResult CreateOrUpdateStaff_JSON(Staff staff)
        {
            string converted = JsonConvert.SerializeObject(
                CRUD.StaffCRUD.CreateOrUpdateStaff(this, staff),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }
    }
}