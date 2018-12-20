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
using System.Web.Mvc;

namespace SaleManagement.Controllers.Statistic
{
    public class SaleStatistic
    {
        public SaleStatistic()
        {
 
        }

        public static ApplicationDbContext _context = new ApplicationDbContext();

        public static List<SaleHistory> GetProductSaleHistory(int? productID, DateTime? dateFrom, DateTime? dateTo)
        {
            var product_query = (
                from product in _context.Product
                select product
                );

            var saleBill_query = (
                from saleBill in _context.SaleBill
                select saleBill
                );

            var saleBillDetail_query = (
                from saleBillDetail in _context.SaleBillDetail
                select saleBillDetail
                );

            if (productID != null)
            {
                saleBillDetail_query = (from x in saleBillDetail_query where x.ProductID == productID select x);
                product_query = (from x in product_query where x.ID == productID select x);
            }

            if (dateFrom != null)
            {
                saleBill_query = (from x in saleBill_query where x.DateCreated >= dateFrom select x);
            }

            if (dateTo != null)
            {
                saleBill_query = (from x in saleBill_query where x.DateCreated <= dateTo select x);
            }

            List<SaleHistory> saleHistoryList = (
                from saleBill in saleBill_query
                join saleBillDetail in saleBillDetail_query on saleBill.ID equals saleBillDetail.SaleBillID
                join product in product_query on saleBillDetail.ProductID equals product.ID
                select new SaleHistory()
                {
                    ProductID = saleBillDetail.ProductID,
                    ProductCode = product.Code,
                    ProductName = product.Name,
                    Date = saleBill.DateCreated,
                    Amount = saleBillDetail.Amount,
                    Price = saleBillDetail.Price
                }
                ).OrderBy(m => m.Date).ThenBy(m => m.ProductID).ToList();

            return saleHistoryList;
        }

        public static List<Product_Amount> GetBestSellingProducts(DateTime? dateFrom, DateTime? dateTo)
        {
            var saleBillDetails = (
                from saleBillDetail in _context.SaleBillDetail
                select saleBillDetail
                );

            if (dateFrom != null)
            {
                saleBillDetails = (from saleBillDetail in saleBillDetails
                                   join saleBill in _context.SaleBill on saleBillDetail.SaleBillID equals saleBill.ID
                                   where saleBill.DateCreated >= dateFrom
                                   select saleBillDetail
                                   );
            }

            if (dateTo != null)
            {
                saleBillDetails = (from saleBillDetail in saleBillDetails
                                   join saleBill in _context.SaleBill on saleBillDetail.SaleBillID equals saleBill.ID
                                   where saleBill.DateCreated <= dateTo
                                   select saleBillDetail
                                   );
            }

            var productID_and_sumSaleAmount_query = (from saleBillDetail in saleBillDetails
                                                     group saleBillDetail by saleBillDetail.ProductID into saleBillDetail_groupBy_productID
                                                     select new
                                                     {
                                                         ProductID = saleBillDetail_groupBy_productID.Key,
                                                         Sum = saleBillDetail_groupBy_productID.Sum(m => m.Amount),
                                                     }
                );

            var saleHistory = (
                from productID_and_sumSaleAmount in productID_and_sumSaleAmount_query
                join product in _context.Product on productID_and_sumSaleAmount.ProductID equals product.ID
                select new Product_Amount()
                {
                    ProductID = productID_and_sumSaleAmount.ProductID,
                    ProductCode = product.Code,
                    ProductName = product.Name,
                    SumAmount = productID_and_sumSaleAmount.Sum,
                }
            ).OrderByDescending(m => m.SumAmount);

            return saleHistory.ToList();
        }

        public static List<Product_Amount> GetLeastSoldProducts(DateTime? dateFrom, DateTime? dateTo)
        {
            var saleBillDetails = (
                from saleBillDetail in _context.SaleBillDetail
                select saleBillDetail
                );

            if (dateFrom != null)
            {
                saleBillDetails = (from saleBillDetail in saleBillDetails
                                   join saleBill in _context.SaleBill on saleBillDetail.SaleBillID equals saleBill.ID
                                   where saleBill.DateCreated >= dateFrom
                                   select saleBillDetail
                                   );
            }

            if (dateTo != null)
            {
                saleBillDetails = (from saleBillDetail in saleBillDetails
                                   join saleBill in _context.SaleBill on saleBillDetail.SaleBillID equals saleBill.ID
                                   where saleBill.DateCreated <= dateTo
                                   select saleBillDetail
                                   );
            }

            var productID_and_sumSaleAmount_query = (from saleBillDetail in saleBillDetails
                                                     group saleBillDetail by saleBillDetail.ProductID into saleBillDetail_groupBy_productID
                                                     select new
                                                     {
                                                         ProductID = saleBillDetail_groupBy_productID.Key,
                                                         Sum = saleBillDetail_groupBy_productID.Sum(m => m.Amount),
                                                     }
                );

            var xxxx = (
                from product in _context.Product
                from productID_and_sumSaleAmount in productID_and_sumSaleAmount_query
                     .Where(productID_and_sumSaleAmount => productID_and_sumSaleAmount.ProductID == product.ID)
                     .DefaultIfEmpty()
                select new Product_Amount()
                {
                    ProductID = product.ID,
                    ProductCode = product.Code,
                    ProductName = product.Name,
                    SumAmount = productID_and_sumSaleAmount == null ? 0 : productID_and_sumSaleAmount.Sum,
                }
            ).OrderBy(m => m.SumAmount);

            return xxxx.ToList();
        }

        /// <summary>
        /// Trả ra tổng giá trị hóa đơn bán hàng trong 1 tháng xác định.
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int GetTotalSaleValueCertainMonth(int storeID, int month, int year)
        {
            DateTime startTime = new DateTime(year, month, 1);
            DateTime endTime = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            var saleBillQuery = (
                from saleBill in _context.SaleBill
                where saleBill.DateCreated >= startTime && saleBill.DateCreated <= endTime
                select saleBill
                );

            int sum = saleBillQuery.ToList().Sum(m => m.TotalValue);
            

            return sum;
        }

        /// <summary>
        /// Trả ra một danh sách các object, mỗi object chứa số hiệu tháng (int) , số hiệu năm (int) , tổng giá trị hóa đơn bán trong tháng đó.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="startMonth"></param>
        /// <param name="startYear"></param>
        /// <param name="endMonth"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        public static List<MonthYear_Amount> GetTotalSaleValueInMonths(Controller controller, int startMonth, int startYear, int endMonth, int endYear)
        {
            int storeID = Int32.Parse(controller.GetSession(Session.SessionKey.StoreID));

            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, DateTime.DaysInMonth(endYear, endMonth));

            DateTime iterator = startDate;
            List<DateTime> monthsToConsider = new List<DateTime>();
            while (true)
            {
                if (iterator > endDate) break;
                monthsToConsider.Add(iterator);
                iterator = iterator.AddMonths(1);
            }

            List<MonthYear_Amount> stats = new List<MonthYear_Amount>();
            foreach (DateTime month in monthsToConsider)
            {
                stats.Add(new MonthYear_Amount()
                {
                    Month = month.Month,
                    Year = month.Year,
                    Amount = GetTotalSaleValueCertainMonth(storeID, month.Month, month.Year),
                });
            }

            return stats;
        }
    }


}