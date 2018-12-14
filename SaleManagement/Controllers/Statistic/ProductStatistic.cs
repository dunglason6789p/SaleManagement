using SaleManagement.Models.DAL;
using SaleManagement.Models.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleManagement.Controllers.Statistic
{
    public class ProductStatistic
    {
        private static ApplicationDbContext _context = new ApplicationDbContext();

        public static List<ProductAvailabilityCheck> GetProductAvailabilityHistory(int productID)
        {
            return _context.ProductAvailabilityCheck.Where(m => m.ProductID == productID).OrderBy(m => m.Date).ToList();
        }
    }


}