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
                     .Where(productID_and_sumSaleAmount => productID_and_sumSaleAmount.ProductID == product.ID )
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
}