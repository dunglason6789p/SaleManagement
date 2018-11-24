using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleManagement.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tên đơn vị hàng (gói/chai/kg/ ...)
        /// </summary>
        public string UnitName { get; set; }        

        /// <summary>
        /// Giá bán
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Giá (chi phí) nhập trung bình
        /// </summary>
        public string AverageCost { get; set; }

        /// <summary>
        /// Giảm giá (%)
        /// </summary>
        public int DiscountRate { get; set; }

        /// <summary>
        /// Mô tả ngắn gọn
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Xuất xứ (từ quốc gia nào).
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Tên nhãn hàng (Apple, Samsung, Omachi, ...)
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Số hàng còn trong kho (cũng là số hàng có thể bán).
        /// </summary>
        public int Availability { get; set; }

        /// <summary>
        /// URL của ảnh sản phẩm
        /// </summary>
        public virtual ICollection<string> Images { get; set; }

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