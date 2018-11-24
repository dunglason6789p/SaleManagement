using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleManagement.Models
{
    public class DetailSaleBill
    {
        /// <summary>
        /// Mã hóa đơn mà Chi tiết hóa đơn này kết nối tới. Đây là 1 phần của khóa chính.
        /// </summary>
        [Key, Column(Order = 0)]
        public int SaleBillID { get; set; }        
        private SaleBill _saleBill;
        [ForeignKey("SaleBillID")]
        public SaleBill SaleBill
        {
            get => _saleBill; set
            {
                _saleBill = value;
                SaleBillID = value.ID; //Để cho khi gán object thì gán luôn cả ID (khóa ngoại).
            }
        }

        /// <summary>
        /// Mã sản phẩm. Đây là 1 phần của khóa chính.
        /// </summary>
        [Key, Column(Order = 1)]
        public int ProductID { get; set; }
        private Product _product { get; set; }
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