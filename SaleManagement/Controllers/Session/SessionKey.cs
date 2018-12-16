using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SaleManagement.Controllers.Session
{
    public static class SessionKey //Map magic-string to lintable names.
    {
        public const String UserName = "UserName";
        public const String StoreID = "StoreID";

        
        public const String SaleBill_Search_code = "SaleBill_Search_code";
        public const String SaleBill_Search_dateFrom = "SaleBill_Search_dateFrom";
        public const String SaleBill_Search_dateTo = "SaleBill_Search_dateTo";
        public const String SaleBill_Search_customerName = "SaleBill_Search_customerName";
        public const String SaleBill_Search_discountFrom = "SaleBill_Search_discountFrom";
        public const String SaleBill_Search_discountTo = "SaleBill_Search_discountTo";
        public const String SaleBill_OrderBy = "SaleBill_OrderBy";
        public const String SaleBill_PageSize = "SaleBill_PageSize";
        public const String SaleBill_PageToGo = "SaleBill_PageToGo";

        public const String ImportBill_OrderBy = "ImportBillStatistic_OrderBy";

        public const String Product_OrderBy = "Product_OrderBy";
    }
}