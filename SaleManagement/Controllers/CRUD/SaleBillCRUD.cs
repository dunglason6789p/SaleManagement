using SaleManagement.Models;
using SaleManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

using SaleManagement.Controllers.Session;
using PagedList;
using System.Globalization;
namespace SaleManagement.Controllers.CRUD
{
    public class SaleBillCRUD
    {
        public static ApplicationDbContext _context = new ApplicationDbContext();

        public static List<SaleBillExtended> GetSaleBillList(Controller controller, string code, string dateFrom, string dateTo, string customerName, string staffName, int? totalValueFrom, int? totalValueTo, int? discountFrom, int? discountTo, string orderBy, int? pageSize, int? pageToGo)
        {
            int storeID = Int32.Parse(controller.GetSession("StoreID").ToString());
            //IQueryable<SaleBill> saleBills = _context.SaleBill.Include(m => m.Customer).Include(m => m.Staff).Include(m => m.SaleBillDetails).Where(m => m.StoreID == storeID);
            IQueryable<SaleBillExtended> saleBills = (
                from saleBill in _context.SaleBill
                join customer in _context.Customer on saleBill.CustomerID equals customer.ID
                join staff in _context.Staff on saleBill.StaffID equals staff.ID
                select new SaleBillExtended()
                {
                    ID = saleBill.ID,
                    Code = saleBill.Code,
                    CustomerID = saleBill.CustomerID,
                    CustomerName = customer.FullName,
                    StaffName = staff.FullName,
                    TotalValue = saleBill.TotalValue,
                    PaymentBank = saleBill.PaymentBank,
                    PaymentCash = saleBill.PaymentCash,
                    DateCreated = saleBill.DateCreated,
                    DiscountValue = saleBill.DiscountValue,
                    StoreID = saleBill.StoreID,

                }
            );

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
                DateTime dateTo_converted = DateTime.Parse(dateTo);
                saleBills = saleBills.Where(m => m.DateCreated >= dateTo_converted);
            }
            if (!String.IsNullOrEmpty(customerName))
            {
                saleBills = saleBills.Where(m => m.CustomerName.Contains(customerName));
            }
            if (!String.IsNullOrEmpty(staffName))
            {
                saleBills = saleBills.Where(m => m.StaffName.Contains(staffName));
            }
            if (totalValueFrom != null)
            {
                saleBills = saleBills.Where(m => m.TotalValue >= totalValueFrom);
            }
            if (totalValueTo != null)
            {
                saleBills = saleBills.Where(m => m.TotalValue >= totalValueTo);
            }
            if (discountTo != null)
            {
                saleBills = saleBills.Where(m => m.DiscountValue <= discountTo);
            }
            if (discountFrom != null)
            {
                saleBills = saleBills.Where(m => m.DiscountValue >= discountFrom);
            }
            if (discountTo != null)
            {
                saleBills = saleBills.Where(m => m.DiscountValue <= discountTo);
            }
            else
            {
                switch (orderBy)
                {
                    case "Code":
                        if (controller.GetSession(SessionKey.SaleBill_OrderBy) == "Code")
                        {
                            saleBills = saleBills.OrderByDescending(m => m.Code);
                            controller.SetSession(SessionKey.SaleBill_OrderBy, "Code_Desc");
                        }
                        else
                        {
                            saleBills = saleBills.OrderBy(m => m.Code);
                            controller.SetSession(SessionKey.SaleBill_OrderBy, "Code");
                        }
                        break;
                    case "DateCreated":
                        if (controller.GetSession(SessionKey.SaleBill_OrderBy) == "DateCreated")
                        {
                            saleBills = saleBills.OrderByDescending(m => m.DateCreated);
                            controller.SetSession(SessionKey.SaleBill_OrderBy, "DateCreated_Desc");
                        }
                        else
                        {
                            saleBills = saleBills.OrderBy(m => m.DateCreated);
                            controller.SetSession(SessionKey.SaleBill_OrderBy, "DateCreated");
                        }
                        break;
                    case "TotalValue":
                        if (controller.GetSession(SessionKey.SaleBill_OrderBy) == "TotalValue")
                        {
                            saleBills = saleBills.OrderByDescending(m => m.TotalValue);
                            controller.SetSession(SessionKey.SaleBill_OrderBy, "TotalValue_Desc");
                        }
                        else
                        {
                            saleBills = saleBills.OrderBy(m => m.TotalValue);
                            controller.SetSession(SessionKey.SaleBill_OrderBy, "TotalValue");
                        }
                        break;
                    default:
                        saleBills = saleBills.OrderBy(m => m.DateCreated);
                        controller.SetSession(SessionKey.SaleBill_OrderBy, "DateCreated");
                        break;
                }
            }

            if (pageSize == null) pageSize = 1000;
            if (pageToGo == null) pageToGo = 1;

            List<SaleBillExtended> saleBillsList = saleBills.ToPagedList(pageToGo.Value, pageSize.Value).ToList();
            //List<SaleBillExtended> saleBillsList = saleBills.ToList(); //DEBUG

            return saleBillsList;
        }




        public static List<SaleBill> GetSaleBillListOLD(Controller controller, string code, string dateFrom, string dateTo, string customerID, string orderBy, int? pageSize, int? pageToGo)
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
                        if (controller.GetSession(SessionKey.SaleBill_OrderBy) == "Code")
                        {
                            saleBills = saleBills.OrderByDescending(m => m.Code);
                            controller.SetSession(SessionKey.SaleBill_OrderBy, "Code_Desc");
                        }
                        else
                        {
                            saleBills = saleBills.OrderBy(m => m.Code);
                            controller.SetSession(SessionKey.SaleBill_OrderBy, "Code");
                        }
                        break;
                    case "DateCreated":
                        if (controller.GetSession(SessionKey.SaleBill_OrderBy) == "DateCreated")
                        {
                            saleBills = saleBills.OrderByDescending(m => m.DateCreated);
                            controller.SetSession(SessionKey.SaleBill_OrderBy, "DateCreated_Desc");
                        }
                        else
                        {
                            saleBills = saleBills.OrderBy(m => m.DateCreated);
                            controller.SetSession(SessionKey.SaleBill_OrderBy, "DateCreated");
                        }
                        break;
                    case "TotalValue":
                        if (controller.GetSession(SessionKey.SaleBill_OrderBy) == "TotalValue")
                        {
                            saleBills = _context.SaleBill.OrderByDescending(m => m.TotalValue);
                            controller.SetSession(SessionKey.SaleBill_OrderBy, "TotalValue_Desc");
                        }
                        else
                        {
                            saleBills = _context.SaleBill.OrderBy(m => m.TotalValue);
                            controller.SetSession(SessionKey.SaleBill_OrderBy, "TotalValue");
                        }
                        break;
                    default:
                        saleBills = saleBills.OrderBy(m => m.DateCreated);
                        controller.SetSession(SessionKey.SaleBill_OrderBy, "DateCreated");
                        break;
                }
            }

            if (pageSize==null) pageSize = 1000;
            if (pageToGo == null) pageToGo = 1;

            List<SaleBill> saleBillsList = saleBills.ToPagedList(pageToGo.Value, pageSize.Value).ToList();
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

        public static List<SaleBillDetail> GetSaleBillDetail(Controller controller, int id)
        {
            int storeID = Int32.Parse(controller.GetSession("StoreID"));
            List<SaleBillDetail> saleBillDetails = _context.SaleBillDetail.Where(m => m.SaleBillID == id && m.StoreID == storeID).ToList();
            foreach(var saleBillDetail in saleBillDetails)
            {
                int productID = saleBillDetail.ProductID;
                Product product = _context.Product.SingleOrDefault(m => m.ID == productID);
                saleBillDetail.Product = product;
            }
            return saleBillDetails;
        }

        public static SaleBill GetSaleBillWithDetail(Controller controller, int id)
        {
            int storeID = Int32.Parse(controller.GetSession("StoreID"));
            SaleBill saleBill = _context.SaleBill.SingleOrDefault(m => m.ID == id);
            List<SaleBillDetail> saleBillDetails = _context.SaleBillDetail.Where(m => m.SaleBillID == id && m.StoreID == storeID).ToList();
            foreach (var saleBillDetail in saleBillDetails)
            {
                int productID = saleBillDetail.ProductID;
                Product product = _context.Product.SingleOrDefault(m => m.ID == productID);
                saleBillDetail.Product = product;
            }
            saleBill.SaleBillDetails = saleBillDetails;

            int staffID = saleBill.StaffID;
            Staff staff = _context.Staff.SingleOrDefault(m => m.ID == staffID);
            saleBill.Staff = staff;

            int customerID = saleBill.CustomerID;
            Customer customer = _context.Customer.SingleOrDefault(m => m.ID == customerID);
            saleBill.Customer = customer;

            return saleBill;
        }
    }
}