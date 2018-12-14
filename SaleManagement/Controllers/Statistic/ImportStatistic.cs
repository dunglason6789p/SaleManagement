using SaleManagement.Controllers.Session;
using SaleManagement.Models;
using SaleManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PagedList;
using System.Globalization;
using System.Web.Mvc;

namespace SaleManagement.Controllers.Statistic
{
    public class ImportStatistic
    {
        private static ApplicationDbContext _context = new ApplicationDbContext();

        public static List<Date_Amount_MoneyInt> GetProductImportHistory(int productID)
        {
            List<Date_Amount_MoneyInt> histories = (from importBillDetail in _context.ImportBillDetail
                                                    join importBill in _context.ImportBill
                                                    on importBillDetail.ImportBillID equals importBill.ID
                                                    where importBillDetail.ProductID == productID
                                                    select new Date_Amount_MoneyInt()
                                                    {
                                                        Date = importBill.DateCreated,
                                                        Amount = importBillDetail.Amount,
                                                        Money = importBillDetail.Price,
                                                    }).OrderBy(h => h.Date).ToList();
            return histories;
        }

        public static List<Date_Amount_MoneyDouble> GetProductAverageCostHistory(int productID)
        {
            List<Date_Amount_MoneyInt> importHistories = GetProductImportHistory(productID); //Sorted by date !.
            List<Date_Amount_MoneyDouble> averCostHistories = new List<Date_Amount_MoneyDouble>();


            double averageCost = 0;
            int availability = 0;
            for (int i = 0; i < importHistories.Count; i++)
            {
                averageCost = (averageCost * availability + importHistories[i].Money * importHistories[i].Amount) / (availability + importHistories[i].Amount);
                availability += importHistories[i].Amount;

                averCostHistories.Add(new Date_Amount_MoneyDouble()
                {
                    Date = importHistories[i].Date,
                    Money = averageCost,
                });
            }

            return averCostHistories;
        }

        /// <summary>
        /// Trả ra tổng giá trị hóa đơn bán hàng trong 1 tháng xác định.
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int GetTotalImportValueCertainMonth(int storeID, int month, int year)
        {
            DateTime startTime = new DateTime(year, month, 1);
            DateTime endTime = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            var importBillQuery = (
                from importBill in _context.ImportBill
                where importBill.DateCreated >= startTime && importBill.DateCreated <= endTime
                select importBill
                );

            int sum = importBillQuery.Sum(m => m.TotalValue);

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
        public static List<MonthYear_Amount> GetTotalImportValueInMonths(Controller controller, int startMonth, int startYear, int endMonth, int endYear)
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
                    Amount = GetTotalImportValueCertainMonth(storeID, month.Month, month.Year),
                });
            }

            return stats;
        }
    }




}