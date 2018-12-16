using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleManagement.Controllers.CRUD
{
    /*
    public class SaleBillExtended : SaleManagement.Models.SaleBill
    {
        public string CustomerName { get; set; }
        public string StaffName { get; set; } 
    }
    */

    public class SaleBillExtended
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int StaffID { get; set; } 
        public string StaffName { get; set; }
        public int TotalValue { get; set; }
        public int PaymentBank { get; set; }
        public int PaymentCash { get; set; }
        public DateTime DateCreated { get; set; }
        public int DiscountValue { get; set; }
        public int StoreID { get; set; } 
    }
}