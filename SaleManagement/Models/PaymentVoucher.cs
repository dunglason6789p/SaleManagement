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

        [Display(Name = "Mã nhân viên")]
        public int StaffID { get; set; }
        [NotMapped]
        public Staff Staff { get; set; }

        [Display(Name = "Mã nhà cung cấp")]
        public int SupplierID { get; set; }
        [NotMapped]
        public Supplier Supplier { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Số tiền")]
        public int Value { get; set; }

        [Display(Name = "Miêu tả")]
        public string Description { get; set; }

        /// <summary>
        /// Mã cửa hàng.
        /// </summary>
        [Display(Name = "Mã cửa hàng")]
        public int StoreID { get; set; }
        [NotMapped]
        private Store _store;
        [NotMapped]
        public virtual Store Store
        {
            get => _store; set
            {
                _store = value;
                if (value != null) StoreID = value.ID; //Để cho khi gán object thì gán luôn cả ID (khóa ngoại).
            }
        }
    }
}