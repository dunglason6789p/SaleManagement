using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SaleManagement.Models;
using SaleManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public ActionResult CreateNew()
        {
            Product product = new Product();
            return View("~/Views/admin_1_rounded/ecommerce_products_edit.cshtml", product);
        }
        public ActionResult Create()
        {
            return View("~/Views/admin_1_rounded/ecommerce_products.cshtml");
        }

        public ActionResult Delete(int ID)
        {
            return View();
        }

        public ActionResult CreateOrUpdate(int ID, String Code, String Name, String UnitName, int RetailPrice, int WholesalePrice, int WholesaleMinAmount, double AverageCost, int DiscountRate,
            int Availability, String CategoryName, String Origin, String BrandName, String DateCreated, int Status, String Image,
            String Description, int StoreID, String type)
        {
            DateTime dateTime = DateTime.Parse(DateCreated);
            Product product = _context.Product.SingleOrDefault(m => m.ID == ID);
            if (product != null)
            {
                product.ID = ID;
                product.Code = Code;
                product.Name = Name;
                product.UnitName = UnitName;
                product.RetailPrice = RetailPrice;
                product.WholesalePrice = WholesalePrice;
                product.WholesaleMinAmount = WholesaleMinAmount;
                product.AverageCost = AverageCost;
                product.DiscountRate = DiscountRate;
                product.Availability = Availability;
                product.CategoryName = CategoryName;
                product.Origin = Origin;
                product.BrandName = BrandName;
                product.DateCreated = dateTime;
                product.Status = Status;
                product.Image = Image;
                product.Description = Description;
                product.StoreID = StoreID;
                int id = CRUD.ProductCRUD.EditProduct(product);
                if (type == "continue")
                {
                    return CreateNew();
                }
                else
                {
                    return View("~/Views/admin_1_rounded/ecommerce_products.cshtml");
                }
            } else
            {
                product = new Product();
                product.ID = ID;
                product.Code = Code;
                product.Name = Name;
                product.UnitName = UnitName;
                product.RetailPrice = RetailPrice;
                product.WholesalePrice = WholesalePrice;
                product.WholesaleMinAmount = WholesaleMinAmount;
                product.AverageCost = AverageCost;
                product.DiscountRate = DiscountRate;
                product.Availability = Availability;
                product.CategoryName = CategoryName;
                product.Origin = Origin;
                product.BrandName = BrandName;
                product.DateCreated = dateTime;
                product.Image = Image;
                product.Description = Description;
                product.StoreID = StoreID;
                int id = CRUD.ProductCRUD.CreateProduct(product);
                if (type == "continue")
                {
                    return CreateNew();
                } else
                {
                    return View("~/Views/admin_1_rounded/ecommerce_products.cshtml");
                }
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