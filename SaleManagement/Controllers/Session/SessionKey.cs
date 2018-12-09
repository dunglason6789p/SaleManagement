using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleManagement.Controllers.Session
{
    public static class SessionKey //Map magic-string to lintable names.
    {
        public const String UserName = "UserName";

        public const String SaleBillStatistic_OrderBy = "SaleBillStatistic_OrderBy";
        public const String ImportBillStatistic_OrderBy = "ImportBillStatistic_OrderBy";

        public const String Product_OrderBy = "Product_OrderBy";
    }
}