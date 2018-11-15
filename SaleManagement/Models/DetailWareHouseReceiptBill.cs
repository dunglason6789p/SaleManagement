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
        [ForeignKey("WareHouseReceiptBillID")]
        public WareHouseReceiptBill WareHouseReceiptBill { get; set; }

        /// <summary>
        /// Mã sản phẩm. Đây là 1 phần của khóa chính.
        /// </summary>
        [Key, Column(Order = 1)]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }

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
    }
}