using SaleManagement.Controllers.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleManagement.Controllers.Statistic
{
    public class CommonStatistic
    {
        /// <summary>
        /// Trả ra một danh sách các object, mỗi object chứa số hiệu tháng (int) , số hiệu năm (int) , tổng giá trị hóa đơn bán trong tháng đó.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="startMonth"></param>
        /// <param name="startYear"></param>
        /// <param name="endMonth"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        public static List<MonthYear_SumSaleImportRevenue> GetTotalSaleImportInMonths(Controller controller, int? startMonth, int? startYear, int? endMonth, int? endYear)
        {
            int storeID = Int32.Parse(controller.GetSession(Session.SessionKey.StoreID));

            DateTime startDate = (startMonth != null && startYear != null) ? new DateTime(startYear.Value, startMonth.Value, 1) : DateTime.Now.AddYears(-1);
            DateTime endDate = (endMonth != null && endYear != null) ? new DateTime(endMonth.Value, endYear.Value, DateTime.DaysInMonth(endYear.Value, endMonth.Value)) : DateTime.Now;

            DateTime iterator = startDate;
            List<DateTime> monthsToConsider = new List<DateTime>();
            while (true)
            {
                if (iterator > endDate) break;
                monthsToConsider.Add(iterator);
                iterator = iterator.AddMonths(1);
            }

            List<MonthYear_SumSaleImportRevenue> stats = new List<MonthYear_SumSaleImportRevenue>();
            foreach (DateTime month in monthsToConsider)
            {
                int sumSale = SaleStatistic.GetTotalSaleValueCertainMonth(storeID, month.Month, month.Year);
                int sumImport = ImportStatistic.GetTotalImportValueCertainMonth(storeID, month.Month, month.Year);
                int difference = sumSale - sumImport;
                stats.Add(new MonthYear_SumSaleImportRevenue()
                {
                    Month = month.Month,
                    Year = month.Year,
                    TotalSale = sumSale,
                    TotalImport = sumImport,
                    TotalRevenue = difference,
                });
            }

            return stats;
        }
    }
}