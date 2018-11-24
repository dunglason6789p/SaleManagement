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
        [ForeignKey("AdminID")]
        public virtual Admin Admin
        {
            get => _admin; set
            {
                _admin = value;
                AdminID = value.ID;
            }
        }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<DetailSaleBill> DetailSaleBills { get; set; }
        public virtual ICollection<DetailWareHouseReceiptBill> DetailWareHouseReceiptBills { get; set; }
        public virtual ICollection<PaymentVoucher> PaymentVouchers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductGroup> ProductGroups { get; set; }
        public virtual ICollection<ReceiptVoucher> ReceiptVouchers { get; set; }
        public virtual ICollection<SaleBill> SaleBills { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<WareHouseReceiptBill> WareHouseReceiptBills { get; set; }
    }
}