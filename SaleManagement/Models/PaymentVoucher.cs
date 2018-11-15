using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleManagement.Models
{
    public class PaymentVoucher
    {
        [Key]
        public int ID { get; set; }
        
        public int StaffID { get; set; }
        [ForeignKey("StaffID")]
        public Staff Staff { get; set; }

        public int SupplierID { get; set; }
        [ForeignKey("SupplierID")]
        public Supplier Supplier { get; set; }

        public DateTime DateCreated { get; set; }

        public int Value { get; set; }

        public string Description { get; set; }
    }
}