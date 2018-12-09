using SaleManagement.Controllers.Session;
using SaleManagement.Models;
using SaleManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using PagedList;
using System.Web.Mvc;

namespace SaleManagement.Controllers.CRUD
{
    public class ImportBillCRUD
    {
        private static ApplicationDbContext _context = new ApplicationDbContext();

        public static List<ImportBill> GetImportBillList(Controller controller, string code, string dateFrom, string dateTo, string supplierID, string orderBy, int? pageToGo)
        {
            int storeID = Int32.Parse(controller.GetSession("StoreID").ToString());
            IQueryable<ImportBill> importBills = _context.ImportBill.Where(m => m.StoreID == storeID);
            if (!String.IsNullOrEmpty(code))
            {
                importBills = importBills.Where(m => m.Code.ToLower().Contains(code));
            }
            if (!String.IsNullOrEmpty(dateFrom))
            {
                //DateTime dateFrom_converted = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dateFrom_converted = DateTime.Parse(dateFrom);
                importBills = importBills.Where(m => m.DateCreated >= dateFrom_converted);
            }
            if (!String.IsNullOrEmpty(dateTo))
            {
                DateTime dateTo_converted = DateTime.Parse(dateFrom);
                importBills = importBills.Where(m => m.DateCreated >= dateTo_converted);
            }
            if (!String.IsNullOrEmpty(supplierID))
            {
                int customID_converted = Int32.Parse(supplierID);
                importBills = importBills.Where(m => m.SupplierID == customID_converted);
            }
            else
            {
                switch (orderBy)
                {
                    case "Code":
                        if (controller.GetSession(SessionKey.ImportBillStatistic_OrderBy) == "Code")
                        {
                            importBills = importBills.OrderByDescending(m => m.Code);
                            controller.SetSession(SessionKey.ImportBillStatistic_OrderBy, "Code_Desc");
                        }
                        else
                        {
                            importBills = importBills.OrderBy(m => m.Code);
                            controller.SetSession(SessionKey.ImportBillStatistic_OrderBy, "Code");
                        }
                        break;
                    case "DateCreated":
                        if (controller.GetSession(SessionKey.ImportBillStatistic_OrderBy) == "DateCreated")
                        {
                            importBills = importBills.OrderByDescending(m => m.DateCreated);
                            controller.SetSession(SessionKey.ImportBillStatistic_OrderBy, "DateCreated_Desc");
                        }
                        else
                        {
                            importBills = importBills.OrderBy(m => m.DateCreated);
                            controller.SetSession(SessionKey.ImportBillStatistic_OrderBy, "DateCreated");
                        }
                        break;
                    case "TotalValue":
                        if (controller.GetSession(SessionKey.ImportBillStatistic_OrderBy) == "TotalValue")
                        {
                            importBills = _context.ImportBill.Include(m => m.ImportBillDetails).OrderByDescending(m => m.TotalValue);
                            controller.SetSession(SessionKey.ImportBillStatistic_OrderBy, "TotalValue_Desc");
                        }
                        else
                        {
                            importBills = _context.ImportBill.Include(m => m.ImportBillDetails).OrderBy(m => m.TotalValue);
                            controller.SetSession(SessionKey.ImportBillStatistic_OrderBy, "TotalValue");
                        }
                        break;
                    default:
                        importBills = importBills.OrderBy(m => m.DateCreated);
                        controller.SetSession(SessionKey.ImportBillStatistic_OrderBy, "DateCreated");
                        break;
                }
            }

            //Đặt kích thước page và xử lý trường hợp pageToGo == null.
            #region
            int pageSize = 1000;// <-- DEBUG. // NORMAL : 10;
            if (pageToGo == null) pageToGo = 1;
            #endregion

            List<ImportBill> ImportBillsList = importBills.ToPagedList(pageToGo.Value, pageSize).ToList();
            foreach (var item in ImportBillsList)
            {
                item.Supplier = _context.Supplier.SingleOrDefault(m => m.ID == item.SupplierID);
                item.Staff = _context.Staff.SingleOrDefault(m => m.ID == item.StaffID);

                int item_ID_temp = item.ID;
                item.ImportBillDetails = _context.ImportBillDetail.Where(m => m.ImportBillID == item_ID_temp).ToList(); // <-- FOR DEBUG ONLY !
            }

            return ImportBillsList;
        }

        public static int CreateImportBill(ImportBill importBill)
        {
            _context.ImportBill.Add(importBill);
            foreach (var importBill_temp in importBill.ImportBillDetails)
            {
                _context.ImportBillDetail.Add(importBill_temp);
            }
            _context.SaveChanges();

            int supplierID_temp = importBill.SupplierID;
            Supplier supplier = _context.Supplier.SingleOrDefault(m => m.ID == supplierID_temp);
            supplier.Money = importBill.Payment - importBill.GetTotalValue();
            _context.SaveChanges();

            return importBill.ID;
        }
    }
}