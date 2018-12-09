using SaleManagement.Models;
using SaleManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using SaleManagement.Controllers.Session;
using PagedList;
using System.Globalization;
namespace SaleManagement.Controllers.CRUD
{
    public class SaleBillCRUD
    {
        public static ApplicationDbContext _context = new ApplicationDbContext();

        public static List<SaleBill> GetSaleBillList(StatisticController controller, string code, string dateFrom, string dateTo, string customerID, string orderBy, int? pageToGo)
        {
            int storeID = Int32.Parse(controller.GetSession("StoreID").ToString());
            IQueryable<SaleBill> saleBills = _context.SaleBill.Where(m => m.StoreID == storeID);
            if (!String.IsNullOrEmpty(code))
            {
                saleBills = saleBills.Where(m => m.Code.ToLower().Contains(code));
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
            if (!String.IsNullOrEmpty(customerID))
            {
                int customID_converted = Int32.Parse(customerID);
                saleBills = saleBills.Where(m => m.CustomerID == customID_converted);
            }
            else
            {
                switch (orderBy)
                {
                    case "Code":
                        if (controller.GetSession(SessionKey.SaleBillStatistic_OrderBy) == "Code")
                        {
                            saleBills = saleBills.OrderByDescending(m => m.Code);
                            controller.SetSession(SessionKey.SaleBillStatistic_OrderBy, "Code_Desc");
                        }
                        else
                        {
                            saleBills = saleBills.OrderBy(m => m.Code);
                            controller.SetSession(SessionKey.SaleBillStatistic_OrderBy, "Code");
                        }
                        break;
                    case "DateCreated":
                        if (controller.GetSession(SessionKey.SaleBillStatistic_OrderBy) == "DateCreated")
                        {
                            saleBills = saleBills.OrderByDescending(m => m.DateCreated);
                            controller.SetSession(SessionKey.SaleBillStatistic_OrderBy, "DateCreated_Desc");
                        }
                        else
                        {
                            saleBills = saleBills.OrderBy(m => m.DateCreated);
                            controller.SetSession(SessionKey.SaleBillStatistic_OrderBy, "DateCreated");
                        }
                        break;
                    case "TotalValue":
                        if (controller.GetSession(SessionKey.SaleBillStatistic_OrderBy) == "TotalValue")
                        {
                            saleBills = _context.SaleBill.Include(m => m.SaleBillDetails).OrderByDescending(m => m.TotalValue);
                            controller.SetSession(SessionKey.SaleBillStatistic_OrderBy, "TotalValue_Desc");
                        }
                        else
                        {
                            saleBills = _context.SaleBill.Include(m => m.SaleBillDetails).OrderBy(m => m.TotalValue);
                            controller.SetSession(SessionKey.SaleBillStatistic_OrderBy, "TotalValue");
                        }
                        break;
                    default:
                        saleBills = saleBills.OrderBy(m => m.DateCreated);
                        controller.SetSession(SessionKey.SaleBillStatistic_OrderBy, "DateCreated");
                        break;
                }
            }

            #region
            int pageSize = 1000;
            if (pageToGo == null) pageToGo = 1;
            #endregion

            List<SaleBill> saleBillsList = saleBills.ToPagedList(pageToGo.Value, pageSize).ToList();
            foreach (var item in saleBillsList)
            {
                item.Customer = _context.Customer.SingleOrDefault(m => m.ID == item.CustomerID);
                item.Staff = _context.Staff.SingleOrDefault(m => m.ID == item.StaffID);
            }

            return saleBillsList;
        }

        public static int CreateSaleBill(SaleBill saleBill)
        {
            _context.SaleBill.Add(saleBill);
            foreach (var saleBillDetail in saleBill.SaleBillDetails)
            {
                _context.SaleBillDetail.Add(saleBillDetail);
            }            
            _context.SaveChanges();

            int customerID_temp = saleBill.CustomerID;
            Customer customer = _context.Customer.SingleOrDefault(m => m.ID == customerID_temp);
            customer.Money = saleBill.PaymentCash + saleBill.PaymentBank - saleBill.GetPaymentTotal();
            _context.SaveChanges();

            return saleBill.ID;
        }
    }
}