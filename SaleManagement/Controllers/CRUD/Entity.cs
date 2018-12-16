using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleManagement.Controllers.CRUD
{
    public class SaleBillExtended : SaleManagement.Models.SaleBill
    {
        public string CustomerName { get; set; }
        public string StaffName { get; set; } 
    }
}