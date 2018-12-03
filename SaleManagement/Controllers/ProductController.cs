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

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /*
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid) //Nếu dữ liệu không hợp lệ
            {
                return View();
            }

            _context.Product.AddOrUpdate<Product>(m => m.ID, product);

            return View(product);
        }
        */

        [HttpGet]
        public ActionResult Edit(int productID)
        {
            Product product = _context.Product.SingleOrDefault(m => m.ID == productID);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid) //Nếu dữ liệu không hợp lệ
            {
                return View();
            }

            _context.Product.AddOrUpdate<Product>(m => m.ID, product);

            return View(product);
        }

        [HttpPost]
        public ActionResult Create(string nullString)
        {
            Product product = new Product();

            product.Code = Request["Code"];
            product.Name = Request["Name"];
            product.UnitName = Request["UnitName"];
            product.RetailPrice = Int32.Parse(Request["RetailPrice"]);
            product.WholesalePrice = Int32.Parse(Request["WholesalePrice"]);
            product.WholesaleMinAmount = Int32.Parse(Request["WholesaleMinAmount"]);
            product.AverageCost = Double.Parse(Request["AverageCost"]);
            product.DiscountRate = Int32.Parse(Request["DiscountRate"]);
            product.Description = Request["Description"];
            product.Origin = Request["Origin"];
            product.BrandName = Request["BrandName"];
            product.Availability = Int32.Parse(Request["Availability"]);
            product.DateCreated = DateTime.ParseExact(Request["DateCreated"].ToString(), "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
            product.Image = Request["Image"];
            product.StoreID = Int32.Parse(Session["StoreID"].ToString());


            string nextAction = Request["NextAction"];
            switch(nextAction)
            {
                case "ShowResult":
                    return View();
                case "GoToProductList":
                    return View();
                default:
                    return View();
            }
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