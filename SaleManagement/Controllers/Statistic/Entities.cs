using System;

namespace SaleManagement.Controllers.Statistic
{
    public class Date_Amount_MoneyInt
    {
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int Money { get; set; }
    } 

    public class Date_Amount_MoneyDouble
    {
        public DateTime Date { get; set; }
        public double Money { get; set; }
    }

    public class ProductAvailabilityHistory
    {
        public DateTime Date { get; set; }
        public int Amount { get; set; }
    }

    public class SaleHistory
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
    }

    public class Product_Amount
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int SumAmount { get; set; }
    }

    public class MonthYear_Amount
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int Amount { get; set; }
    }

    public class MonthYear_SumSaleImportRevenue
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int TotalSale { get; set; }
        public int TotalImport { get; set; }
        public int TotalRevenue { get; set; }
    }
}