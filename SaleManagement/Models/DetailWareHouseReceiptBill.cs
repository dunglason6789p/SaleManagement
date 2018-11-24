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
    public class DetailWareHouseReceiptBill
    {
        /// <summary>
        /// Mã hóa đơn mà Chi tiết hóa đơn này kết nối tới. Đây là 1 phần của khóa chính.
        /// </summary>
        [Key, Column(Order = 0)]
        public int WareHouseReceiptBillID { get; set; }
        private WareHouseReceiptBill _wareHouseReceiptBill;
        [ForeignKey("WareHouseReceiptBillID")]
        public WareHouseReceiptBill WareHouseReceiptBill
        {
            get => _wareHouseReceiptBill; set
            {
                _wareHouseReceiptBill = value;
                WareHouseReceiptBillID = value.ID;
            }
        }

        /// <summary>
        /// Mã sản phẩm. Đây là 1 phần của khóa chính.
        /// </summary>
        [Key, Column(Order = 1)]
        public int ProductID { get; set; }
        private Product _product;
        [ForeignKey("ProductID")]
        public Product Product
        {
            get => _product; set
            {
                _product = value;
                ProductID = value.ID;
            }
        }

        /// <summary>
        /// Giá bán tại thời điểm lập hóa đơn.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Số lượng.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Mã nhà cung cấp.
        /// </summary>
        public int SupplierID { get; set; }
        [ForeignKey("SupplierID")]
        public Supplier Supplier { get; set; }

        /// <summary>
        /// Mã cửa hàng.
        /// </summary>
        public int StoreID { get; set; }
        private Store _store;
        [ForeignKey("StoreID")]
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