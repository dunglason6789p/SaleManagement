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

        /*
        [HttpPost]
        public ActionResult Create(string nullString)
        {
            Product productDebug = new Product();

            productDebug.Code = Request["Code"];
            productDebug.Name = Request["Name"];
            productDebug.UnitName = Request["UnitName"];
            productDebug.RetailPrice = Int32.Parse(Request["RetailPrice"]);
            productDebug.WholesalePrice = Int32.Parse(Request["WholesalePrice"]);
            productDebug.WholesaleMinAmount = Int32.Parse(Request["WholesaleMinAmount"]);
            productDebug.AverageCost = Double.Parse(Request["AverageCost"]);
            productDebug.DiscountRate = Int32.Parse(Request["DiscountRate"]);
            productDebug.Description = Request["Description"];
            productDebug.Origin = Request["Origin"];
            productDebug.Brand = Request["Brand"];
            productDebug.Availability = Int32.Parse(Request["Availability"]);
            productDebug.Image = Request["Image"];
            productDebug.StoreID = Int32.Parse(Request["StoreID"]);

            Product product = new Product()
            {
                Code = Request["Code"],
                Name = Request["Name"],
                UnitName = Request["UnitName"],
                RetailPrice = Int32.Parse(Request["RetailPrice"]),
                WholesalePrice = Int32.Parse(Request["WholesalePrice"]),
                WholesaleMinAmount = Int32.Parse(Request["WholesaleMinAmount"]),
                AverageCost = Double.Parse(Request["AverageCost"]),
                DiscountRate = Int32.Parse(Request["DiscountRate"]),
                Description = Request["Description"],
                Origin = Request["Origin"],
                Brand = Request["Brand"],
                Availability = Int32.Parse(Request["Availability"]),
                Image = Request["Image"],
                StoreID = Int32.Parse(Request["StoreID"]),
            };

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
        */
    }
}