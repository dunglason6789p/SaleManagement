using SaleManagement.Models;
using SaleManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;

namespace SaleManagement.Controllers
{
    public class SaleBillController : Controller
    {
        private ApplicationDbContext _context;

        public SaleBillController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string nullString)
        {
            return View();
        }

        public ActionResult ViewTest()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexWORKING(string code, string dateFrom, string dateTo, string customerID, string previousOrderBy, string orderBy, int? pageToGo)
        {
            int storeID = Int32.Parse(Session["StoreID"].ToString());
            IQueryable<SaleBill> saleBills = _context.SaleBill.Where(m => m.StoreID == storeID);
            if (!String.IsNullOrEmpty(code))
            {
                saleBills = saleBills.Where(m => m.Code.ToLower().Contains(code));
            }
            if (!String.IsNullOrEmpty(dateFrom))
            {
                DateTime dateFrom_converted = DateTime.ParseExact(dateFrom, "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
                saleBills = saleBills.Where(m => m.DateCreated >= dateFrom_converted);
            }
            if (!String.IsNullOrEmpty(dateTo))
            {
                DateTime dateTo_converted = DateTime.ParseExact(dateTo, "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
                saleBills = saleBills.Where(m => m.DateCreated >= dateTo_converted);
            }
            if (!String.IsNullOrEmpty(customerID))
            {
                int customID_converted = Int32.Parse(customerID);
                saleBills = saleBills.Where(m => m.CustomerID == customID_converted);
            }
            else
            {
                switch(orderBy)
                {
                    case "Code":
                        if (previousOrderBy == "Code")
                        {
                            saleBills = saleBills.OrderByDescending(m => m.Code);
                        } 
                        else
                        {
                            saleBills = saleBills.OrderBy(m => m.Code);
                        }
                        break;
                    case "DateCreated":
                        if (previousOrderBy == "DateCreated")
                        {
                            saleBills = saleBills.OrderByDescending(m => m.DateCreated);
                        }
                        else
                        {
                            saleBills = saleBills.OrderBy(m => m.DateCreated);
                        }
                        break;
                    case "TotalValue":
                        if (previousOrderBy == "TotalValue")
                        {
                            saleBills = _context.SaleBill.Include(m => m.SaleBillDetails).OrderByDescending(m => m.TotalValue);
                        }
                        else
                        {
                            saleBills = _context.SaleBill.Include(m => m.SaleBillDetails).OrderBy(m => m.TotalValue);
                        }
                        break;
                    default:
                        saleBills = saleBills.OrderByDescending(m => m.DateCreated);
                        break;
                }
            }

            //Đặt kích thước page và xử lý trường hợp pageToGo == null.
            #region
            int pageSize = 10;
            if (pageToGo == null) pageToGo = 1;
            #endregion

            //string code, string dateFrom, string dateTo, string customerID, string previousOrderBy, string orderBy, int? pageToGo
            ViewBag.Code = code;
            ViewBag.DateForm = dateFrom;
            ViewBag.DateTo = dateTo;
            ViewBag.CustomerID = customerID;
            ViewBag.PreviousOrderBy = previousOrderBy;
            ViewBag.OrderBy = orderBy;
            ViewBag.PageToGo = pageToGo;

            List<SaleBill> saleBillsList = saleBills.ToPagedList(pageToGo.Value, pageSize).ToList();
            foreach(var item in saleBillsList)
            {
                item.Customer = _context.Customer.SingleOrDefault(m => m.ID == item.CustomerID);
                item.Staff = _context.Staff.SingleOrDefault(m => m.ID == item.StaffID);
            }

            return View("~/Views/TraCuuHocPhan/TimKiem.cshtml", null, saleBillsList);
        }

        public JsonResult CreateSaleBill(SaleBill saleBill)
        {
            int id = CRUD.SaleBillCRUD.CreateSaleBill(saleBill);
            return Json(new {
                ID = id
            });
        }

        
    }
}