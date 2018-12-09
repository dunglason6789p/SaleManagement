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

        /// <summary>
        /// Mã hóa đơn.
        /// </summary>
        [Key]
        public int ID { get; set; }

        //Unique check is done only in application layer (unique by each store).
        public string Code { get; set; }

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
                if (value != null) SupplierID = value.ID; //Để cho khi gán object thì gán luôn cả ID (khóa ngoại).
            }
        }

        public int StaffID { get; set; }
        [NotMapped]
        private Staff _staff;
        [NotMapped]
        public Staff Staff
        {
            get => _staff; set
            {
                _staff = value;
                if (value != null) StaffID = value.ID;
            }
        }

        /// <summary>
        /// Tổng giá trị hóa đơn bán.
        /// </summary>
        [Display(Name = "Tổng giá trị")]
        public int TotalValue { get; set; }

        /// <summary>
        /// Discount by entering coupon code.
        /// </summary>
        [Display(Name = "Lượng % giảm giá")]
        public int DiscountValue { get; set; }

        /// <summary>
        /// Số tiền cửa hàng của mình đã trả cho Supplier.
        /// </summary>
        public int Payment { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; }

        

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

        [NotMapped]
        public virtual ICollection<ImportBillDetail> ImportBillDetails { get; set; }

        public void RefreshTotalValue()
        {
            TotalValue = GetTotalValue();
        }

        public int GetTotalValue()
        {
            int sum = 0;
            if (ImportBillDetails != null)
                foreach (var item in ImportBillDetails)
                {
                    if (item != null) sum += item.Amount * item.Price;
                    else ImportBillDetails.Remove(item);
                }
            return sum;
        }
    }
}