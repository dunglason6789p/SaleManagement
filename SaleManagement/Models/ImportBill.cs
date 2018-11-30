using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleManagement.Models
{
    /// <summary>
    /// Hóa đơn nhập hàng.
    /// </summary>
    public class ImportBill
    {
        [NotMapped]
        private Supplier _supplier;
        [NotMapped]
        private int _totalAmount;
        [NotMapped]
        private int _totalValue;

        /// <summary>
        /// Mã hóa đơn.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// abc.
        /// </summary>
        [Display(Name = "Mã nhà cung cấp")]
        public int SupplierID { get; set; }
        [NotMapped]
        public Supplier Supplier
        {
            get => _supplier; set
            {
                _supplier = value;
                SupplierID = value.ID; //Để cho khi gán object thì gán luôn cả ID (khóa ngoại).
            }
        }

        /// <summary>
        /// Tổng giá trị hóa đơn bán.
        /// </summary>
        [Display(Name = "Tổng giá trị")]
        public int TotalValue
        {
            get
            {
                int sum = 0;
                foreach (var item in ImportBillDetails)
                {
                    sum += item.Price;
                }
                return sum;
            }
            private set { }
        }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Discount by entering coupon code.
        /// </summary>
        [Display(Name = "Lượng % giảm giá")]
        public int DiscountValue { get; set; }

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
                StoreID = value.ID; //Để cho khi gán object thì gán luôn cả ID (khóa ngoại).
            }
        }

        [NotMapped]
        public virtual ICollection<ImportBillDetail> ImportBillDetails { get; set; }
    }
}