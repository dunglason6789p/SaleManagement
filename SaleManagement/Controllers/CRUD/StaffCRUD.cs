using SaleManagement.Controllers.Session;
using SaleManagement.Models;
using SaleManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleManagement.Controllers.CRUD
{
    public class StaffCRUD
    {
        private static ApplicationDbContext _context = new ApplicationDbContext();

        public static List<Staff> SearchStaff(Controller controller, string code, string name, string phoneNumber, string email)
        {
            int storeID = Int32.Parse(controller.GetSession("StoreID").ToString());
            IEnumerable<Staff> staffQuery = _context.Staff.Where(m => m.StoreID == storeID);
            if (!String.IsNullOrEmpty(code))
            {
                staffQuery = staffQuery.Where(m => m.Code.ToLower().Contains(code));
            }
            if (!String.IsNullOrEmpty(name))
            {
                staffQuery = staffQuery.Where(m => m.FullName.ToLower().Contains(name));
            }
            if (!String.IsNullOrEmpty(phoneNumber))
            {
                staffQuery = staffQuery.Where(m => m.Phone.Contains(phoneNumber));
            }
            if (!String.IsNullOrEmpty(email))
            {
                staffQuery = staffQuery.Where(m => m.Email.Contains(email));
            }
            return staffQuery.ToList();
        }

        public static Staff CreateOrUpdateStaff(Controller controller, Staff staff)
        {
            _context.Staff.AddOrUpdate(m => m.ID, staff);
            _context.SaveChanges();

            return staff;
        }
    }
}