using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SaleManagement.Controllers.Statistic;

namespace SaleManagement.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Statistic
        public ActionResult Index()
        {
            return View("~/Views/admin_1_rounded/dashboard_2.cshtml");
        }

        

        public ActionResult GetProductImportHistory_JSON(int productID)
        {
            //return Json(Statistic.ImportStatistic.GetProductImportHistory(productID));

            string converted = JsonConvert.SerializeObject(
                Statistic.ImportStatistic.GetProductImportHistory(productID), 
                Formatting.None, 
                new IsoDateTimeConverter()
            {
                DateTimeFormat = "yyyy-MM-dd"
            });
            return Content(converted, "application/json");
        }

        public ActionResult GetProductAverageCostHistory_JSON(int productID)
        {
            //return Json(Statistic.ImportStatistic.GetProductAverageCostHistory(productID));

            string converted = JsonConvert.SerializeObject(
                Statistic.ImportStatistic.GetProductAverageCostHistory(productID),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }

        public ActionResult GetProductAvailabilityCheckHistory_JSON(int productID)
        {
            string converted = JsonConvert.SerializeObject(
                Statistic.ProductStatistic.GetProductAvailabilityHistory(productID),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }

        public ActionResult GetProductSaleHistory_JSON(int? productID, DateTime? dateFrom, DateTime? dateTo)
        {
            string converted = JsonConvert.SerializeObject(
                Statistic.SaleStatistic.GetProductSaleHistory(productID, dateFrom, dateTo),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }

        public ActionResult GetBestSellingProducts_JSON(DateTime? dateFrom, DateTime? dateTo)
        {
            string converted = JsonConvert.SerializeObject(
                Statistic.SaleStatistic.GetBestSellingProducts(dateFrom, dateTo),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }

        public ActionResult GetLeastSoldProducts_JSON(DateTime? dateFrom, DateTime? dateTo)
        {
            string converted = JsonConvert.SerializeObject(
                Statistic.SaleStatistic.GetLeastSoldProducts(dateFrom, dateTo),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }

        public ActionResult GetTotalSaleImportInMonths_JSON(int? startMonth, int? startYear, int? endMonth, int? endYear)
        {
            string converted = JsonConvert.SerializeObject(
                Statistic.CommonStatistic.GetTotalSaleImportInMonths(this, startMonth, startYear, endMonth, endYear),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }

        public ActionResult GetTotalRevenue_JSON()
        {
            //GetTotalRevenueAllTime
            string converted = JsonConvert.SerializeObject(
                Statistic.CommonStatistic.GetTotalRevenueAllTime(this),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }

        public ActionResult GetCommonInfo_JSON()
        {
            string converted = JsonConvert.SerializeObject(
                new {
                    //Tổng lợi nhuận
                    totalRevenue = Statistic.CommonStatistic.GetTotalRevenueAllTime(this),
                    //Tổng số đơn hàng
                    totalSaleBillCount = Statistic.SaleStatistic.CountSaleBillAllTime(this),
                    //Tổng số người dùng
                    totalCustomer = Statistic.CustomerStatistic.CountCustomer(this),
                    //Tổng giá trị hàng tồn kho
                    totalProductValue = Statistic.ProductStatistic.GetCurrentTotalProductValue(this)
                },
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }
    }
}