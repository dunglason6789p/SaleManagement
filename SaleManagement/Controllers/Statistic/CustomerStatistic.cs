using SaleManagement.Controllers.Session;
using SaleManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleManagement.Controllers.Statistic
{
    public class CustomerStatistic
    {
        private static ApplicationDbContext _context = new ApplicationDbContext();
        public static int CountCustomer(Controller controller)
        {
            int storeID = Int32.Parse(controller.GetSession("StoreID").ToString());
            return _context.Customer.Where(m => m.StoreID == storeID).Count();
        }
    }
}