using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleManagement.Models
{
    /// <summary>
    /// Chi tiết hóa đơn nhập hàng
    /// </summary>
    public class ImportBillDetail
    {
        /// <summary>
        /// Mã hóa đơn mà Chi tiết hóa đơn này kết nối tới. Đây là 1 phần của khóa chính.
        /// </summary>
        [Key, Column(Order = 0)]
        [Display(Name = "Mã hóa đơn nhập")]
        public int ImportBillID { get; set; }
        private ImportBill _importBillID;
        [NotMapped]
        public ImportBill ImportBill
        {
            get => _importBillID; set
            {
                _importBillID = value;
                ImportBillID = value.ID;
            }
        }

        /// <summary>
        /// Mã sản phẩm. Đây là 1 phần của khóa chính.
        /// </summary>
        [Display(Name = "Mã sản phẩm")]
        [Key, Column(Order = 1)]
        public int ProductID { get; set; }
        [NotMapped]
        private Product _product;
        [NotMapped]
        public Product Product
        {
            get => _product; set
            {
                _product = value;
                ProductID = value.ID;
            }
        }

        /// <summary>
        /// Giá nhập tại thời điểm lập hóa đơn.
        /// </summary>
        [Display(Name = "Đơn giá")]
        public int Price { get; set; }

        /// <summary>
        /// Số lượng.
        /// </summary>
        [Display(Name = "Số lượng")]
        public int Amount { get; set; }

        /// <summary>
        /// Mã nhà cung cấp.
        /// </summary>
        [Display(Name = "Mã nhà cung cấp")]
        public int SupplierID { get; set; }
        [NotMapped]
        private Supplier _supplier;
        [NotMapped]
        public Supplier Supplier
        {
            get => _supplier; set
            {
                _supplier = value;
                SupplierID = value.ID;
            }
        }

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
    }
}