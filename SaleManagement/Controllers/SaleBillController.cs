using SaleManagement.Models;
using SaleManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;
using SaleManagement.Controllers.CRUD;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
            return View("~/Views/admin_1_rounded/ecommerce_orders.cshtml");
        }

        public ActionResult AddNewOrder()
        {
            return View("~/Views/admin_1_rounded/ecommerce_orders_add.cshtml");
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

        public ActionResult ViewDetail(int ID)
        {
            ViewBag.ID = ID;
            return View("~/Views/admin_1_rounded/ecommerce_orders_view.cshtml");
        }

        public ActionResult GetSaleBillDetail_JSON(int ID)
        {            
            string converted = JsonConvert.SerializeObject(
                SaleBillCRUD.GetSaleBillDetail(this, ID),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }

        public ActionResult GetSaleBillWithDetail_JSON(int ID)
        {
            string converted = JsonConvert.SerializeObject(
                SaleBillCRUD.GetSaleBillWithDetail(this, ID),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }

        public ActionResult Index()
        {
            return View();
        }

        //JSON.
        /// <summary>
        /// Tìm kiếm hóa đơn bán.
        /// </summary>
        /// <param name="code">Mã hóa đơn</param>
        /// <param name="dateFrom">Lọc các hóa đơn sau ngày này.</param>
        /// <param name="dateTo">Lọc các hóa đơn trước ngày này.</param>
        /// <param name="customerID">Mã khách hàng</param>
        /// <param name="orderBy">Tiêu chí sắp xếp ("Code"=theo mã hóa đơn, "DateFrom"=theo ngày, "TotalValue"=theo giá trị hóa đơn.)</param>
        /// <param name="pageSize">Số kết quả tìm kiếm trong 1 trang</param>
        /// <param name="pageToGo">Trang muốn đến.</param>
        /// <returns></returns>
        public ActionResult SearchSaleBill(string code, string dateFrom, string dateTo, string customerName, string staffName, int? totalValueFrom, int? totalValueTo, int? discountFrom, int? discountTo, string orderBy, int? pageSize, int? pageToGo)
        {
            string converted = JsonConvert.SerializeObject(
                SaleBillCRUD.GetSaleBillList(this, code, dateFrom, dateTo, customerName, staffName, totalValueFrom, totalValueTo, discountFrom, discountTo, orderBy, pageSize, pageToGo),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }

        //JSON.
        public ActionResult CreateSaleBill_OLD(SaleBill saleBill)
        {
            int id = CRUD.SaleBillCRUD.CreateSaleBill(saleBill);
            return Json(new {
                ID = id
            });
        }

        //JSON.
        public ActionResult CreateSaleBill(string listProduct, string amount, string total, string customer)
        {
            /*
            var listProductDefinition = new
            {
                ID = "",
                Code = "",
                RetailPrice = ""
            };
            var listProductConverted = JsonConvert.DeserializeAnonymousType(listProduct, listProductDefinition);
            */

            dynamic listProductConverted = JsonConvert.DeserializeObject(listProduct);

            dynamic amountConverted = JsonConvert.DeserializeObject(amount);

            dynamic customerConverted = JsonConvert.DeserializeObject(customer);

            SaleBill saleBill = new SaleBill();

            saleBill.CustomerID = customerConverted.ID;

            List<SaleBillDetail> saleBillDetails = new List<SaleBillDetail>();

            for(int i=0; i<listProductConverted.Count; i++)
            {
                saleBillDetails.Add(new SaleBillDetail()
                {
                    ProductID = listProductConverted[i].ID,
                    Amount = amountConverted[i],
                    Price = listProductConverted[i].RetailPrice
                });
            }

            saleBill.SaleBillDetails = saleBillDetails;

            int sum = 0; 
            foreach(var item in saleBillDetails)
            {
                sum += item.Amount * item.Price;
            }
            saleBill.TotalValue = sum;

            _context.SaveChanges();


            int id = CRUD.SaleBillCRUD.CreateSaleBill(saleBill);
            return Json(new
            {
                saleBill
            });
        }


    }
}