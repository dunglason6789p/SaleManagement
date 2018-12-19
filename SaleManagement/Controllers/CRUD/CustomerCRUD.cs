using SaleManagement.Controllers.Session;
using SaleManagement.Models;
using SaleManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleManagement.Controllers.CRUD
{
    public class CustomerCRUD
    {
        private static ApplicationDbContext _context = new ApplicationDbContext();
        public static List<Customer> SeachCustomer(Controller controller, string phoneNumber, string name)
        {
            int storeID = Int32.Parse(controller.GetSession("StoreID").ToString());
            IEnumerable<Customer> customerQuery = _context.Customer.Where(m => m.StoreID == storeID);
            if (!String.IsNullOrEmpty(name))
            {
                customerQuery = customerQuery.Where(m => m.FullName.ToLower().Contains(name.ToLower()));
            }
            if (!String.IsNullOrEmpty(phoneNumber))
            {
                customerQuery = customerQuery.Where(m => m.Phone.Contains(phoneNumber));
            }
            return customerQuery.ToList();
        }
    }
}