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
    }
}