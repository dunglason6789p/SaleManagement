using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleManagement.Models
{
    public class Store
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public int AdminID { get; set; }
        private Admin _admin;
        [NotMapped]
        public virtual Admin Admin
        {
            get => _admin; set
            {
                _admin = value;
                if (value != null) AdminID = value.ID;
            }
        }

        [NotMapped]
        public virtual ICollection<Category> Categories { get; set; }
        [NotMapped]
        public virtual ICollection<Customer> Customers { get; set; }
        [NotMapped]
        public virtual ICollection<SaleBillDetail> SaleBillDetails { get; set; }
        [NotMapped]
        public virtual ICollection<ImportBillDetail> ImportBillDetails { get; set; }
        [NotMapped]
        public virtual ICollection<PaymentVoucher> PaymentVouchers { get; set; }
        [NotMapped]
        public virtual ICollection<Product> Products { get; set; }       
        [NotMapped]
        public virtual ICollection<ReceiptVoucher> ReceiptVouchers { get; set; }
        [NotMapped]
        public virtual ICollection<SaleBill> SaleBills { get; set; }
        [NotMapped]
        public virtual ICollection<Staff> Staffs { get; set; }
        [NotMapped]
        public virtual ICollection<Supplier> Suppliers { get; set; }
        [NotMapped]
        public virtual ICollection<ImportBill> ImportBills { get; set; }
    }
}