using SaleManagement.Controllers.Session;
using SaleManagement.Models.DAL;
using SaleManagement.Models.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleManagement.Controllers.Statistic
{ 
    public class ProductStatistic
    {
        private static ApplicationDbContext _context = new ApplicationDbContext();

        public static List<ProductAvailabilityCheck> GetProductAvailabilityHistory(int productID)
        {
            return _context.ProductAvailabilityCheck.Where(m => m.ProductID == productID).OrderBy(m => m.Date).ToList();
        }

        public static int GetCurrentTotalProductValue(Controller controller)
        {
            int storeID = Int32.Parse(controller.GetSession("StoreID").ToString());
            double total = _context.Product.Where(m => m.StoreID == storeID).ToList().Sum(m => m.AverageCost);
            return Convert.ToInt32(total);
        }
    }


}