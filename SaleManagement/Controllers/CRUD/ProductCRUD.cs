using SaleManagement.Controllers.Session;
using SaleManagement.Models;
using SaleManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PagedList;
using System.Web.Mvc;
using System.Data.Entity.Migrations;

namespace SaleManagement.Controllers.CRUD
{
    public class ProductCRUD
    {
        public static ApplicationDbContext _context = new ApplicationDbContext();

        public static int CreateProduct(Product product)
        {
            CheckBrandName(product.BrandName);

            _context.Product.Add(product);
            _context.SaveChanges();

            return product.ID;
        }

        public static int EditProduct(Product product)
        {
            CheckBrandName(product.BrandName);

            _context.Product.AddOrUpdate(m => m.ID, product);
            _context.SaveChanges();

            return product.ID;
        }

        private static void CheckBrandName(string brandName)
        {
            if (_context.Brand.SingleOrDefault(m => m.Name == brandName) == null)
            {
                _context.Brand.Add(new Brand() { Name = brandName });
                _context.SaveChanges();
            }
        }

        public static List<Product> GetProductList(Controller controller, string code, string productName, string category, string priceFrom, string priceTo, string availabilityFrom, string availabilityTo, string dateFrom, string dateTo, string brandName, string orderBy, int? pageSize, int? pageToGo)
        {
            int storeID = Int32.Parse(controller.GetSession("StoreID").ToString());
            IQueryable<Product> saleBills = _context.Product.Where(m => m.StoreID == storeID);
            if (!String.IsNullOrEmpty(code))
            {
                saleBills = saleBills.Where(m => m.Code.ToLower().Contains(code.ToLower()));
            }
            if (!String.IsNullOrEmpty(productName))
            {
                saleBills = saleBills.Where(m => m.Name.ToLower().Contains(productName.ToLower()));
            }
            if (!String.IsNullOrEmpty(category))
            {
                saleBills = saleBills.Where(m => m.CategoryName.ToLower().Contains(category.ToLower()));
            }

            if (!String.IsNullOrEmpty(priceFrom))
            {
                int priceConverted = Int32.Parse(priceFrom);
                saleBills = saleBills.Where(m => m.RetailPrice >= priceConverted);
            }
            if (!String.IsNullOrEmpty(priceTo))
            {
                int priceConverted = Int32.Parse(priceTo);
                saleBills = saleBills.Where(m => m.RetailPrice <= priceConverted);
            }

            if (!String.IsNullOrEmpty(availabilityFrom))
            {
                int availConverted = Int32.Parse(availabilityFrom);
                saleBills = saleBills.Where(m => m.Availability >= availConverted);
            }
            if (!String.IsNullOrEmpty(availabilityTo))
            {
                int availConverted = Int32.Parse(availabilityTo);
                saleBills = saleBills.Where(m => m.Availability <= availConverted);
            }


            if (!String.IsNullOrEmpty(dateFrom))
            {
                DateTime dateFrom_converted = DateTime.Parse(dateFrom);
                saleBills = saleBills.Where(m => m.DateCreated >= dateFrom_converted);
            }
            if (!String.IsNullOrEmpty(dateTo))
            {
                DateTime dateTo_converted = DateTime.Parse(dateFrom);
                saleBills = saleBills.Where(m => m.DateCreated >= dateTo_converted);
            }
            if (!String.IsNullOrEmpty(brandName))
            {
                saleBills = saleBills.Where(m => m.BrandName.Contains(brandName));
            }
            else
            {
                switch (orderBy)
                {
                    case "Code":
                        if (controller.GetSession(SessionKey.Product_OrderBy) == "Code")
                        {
                            saleBills = saleBills.OrderByDescending(m => m.Code);
                            controller.SetSession(SessionKey.Product_OrderBy, "Code_Desc");
                        }
                        else
                        {
                            saleBills = saleBills.OrderBy(m => m.Code);
                            controller.SetSession(SessionKey.Product_OrderBy, "Code");
                        }
                        break;
                    case "DateCreated":
                        if (controller.GetSession(SessionKey.Product_OrderBy) == "DateCreated")
                        {
                            saleBills = saleBills.OrderByDescending(m => m.DateCreated);
                            controller.SetSession(SessionKey.Product_OrderBy, "DateCreated_Desc");
                        }
                        else
                        {
                            saleBills = saleBills.OrderBy(m => m.DateCreated);
                            controller.SetSession(SessionKey.Product_OrderBy, "DateCreated");
                        }
                        break;                    
                    default:
                        saleBills = saleBills.OrderBy(m => m.Code);
                        controller.SetSession(SessionKey.Product_OrderBy, "Code");
                        break;
                }
            }

            #region
            pageSize = 1000;
            if (pageToGo == null) pageToGo = 1;
            #endregion

            List<Product> saleBillsList = saleBills.ToPagedList(pageToGo.Value, pageSize.Value).ToList();

            return saleBillsList;
        }
    }
}