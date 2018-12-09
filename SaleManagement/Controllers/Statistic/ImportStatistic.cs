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
            for (int i=0;i<importHistories.Count;i++)
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

        
    }

    
}