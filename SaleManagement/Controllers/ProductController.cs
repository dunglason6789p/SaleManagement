using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SaleManagement.Models;
using SaleManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleManagement.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _context;

        public ProductController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult ViewDetail(int ID)
        {
            Product product = _context.Product.SingleOrDefault(m => m.ID == ID);
            return View("~/Views/admin_1_rounded/ecommerce_products_edit.cshtml", product);
        }
        public ActionResult Create()
        {
            return View("~/Views/admin_1_rounded/ecommerce_products.cshtml");
        }

        public ActionResult CreateOrUpdate(int ID, String Code, String Name, String UnitName, int RetailPrice, int WholesalePrice, int WholesaleMinAmount, double AverageCost, int DiscountRate,
            int Availability, String CategoryName, String Origin, String BrandName, String DateCreated, int Status, )
        {
            Product product = _context.Product.SingleOrDefault(m => m.ID == ID);
            if (product != null)
            {
                // Đoạn này là create đúng k
                return View()
            }
            else
            {
                // trong này là update chuẩn kx`
                return View();
            }
        }

        public JsonResult EditProduct_JSON(Product product)
        {
            int id = CRUD.ProductCRUD.EditProduct(product);
            return Json(new
            {
                ID = id
            });
        }

        public JsonResult CreateProduct_JSON(Product product)
        {
            int id = CRUD.ProductCRUD.CreateProduct(product);
            return Json(new
            {
                ID = id
            });
        }

        public ActionResult SearchProduct_JSON(string code, string dateFrom, string dateTo, string brandName, string orderBy, int? pageToGo)
        {
            string converted = JsonConvert.SerializeObject(
                CRUD.ProductCRUD.GetProductList(this, code, dateFrom, dateTo, brandName, orderBy, pageToGo),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }

        public JsonResult SearchByCode(string searchString)
        {
            List<Product> productList = _context.Product.Where(m => m.Code.StartsWith(searchString)).ToList();
            return Json(productList);
        }

        public JsonResult SearchByName(string searchString)
        {
            List<Product> productList = _context.Product.Where(m => m.Name.StartsWith(searchString)).ToList();
            return Json(productList);
        }
    }
}