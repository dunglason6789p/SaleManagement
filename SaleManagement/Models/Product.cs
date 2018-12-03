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

        //Unique check is done only in application layer (unique by each store).
        public string Code { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tên đơn vị hàng (gói/chai/kg/ ...)
        /// </summary>
        [Display(Name="Đơn vị tính")]
        public string UnitName { get; set; }

        public int RetailPrice { get; set; }

        public int WholesalePrice { get; set; }

        public int WholesaleMinAmount { get; set; }

        /// <summary>
        /// Giá (chi phí) nhập trung bình
        /// </summary>
        public double AverageCost { get; set; }

        /// <summary>
        /// Giảm giá (%)
        /// </summary>
        public int DiscountRate { get; set; }        

        /// <summary>
        /// Số hàng còn trong kho (cũng là số hàng có thể bán).
        /// </summary>
        public int Availability { get; set; }

        /// <summary>
        /// Lượng đã bán
        /// </summary>
        public int AmountSold { get; set; }

        //Không kết nối tới bảng Category, lý do là để đơn giản hóa.
        public string CategoryName { get; set; }

        /// <summary>
        /// Xuất xứ (từ quốc gia nào).
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Tên nhãn hàng (Apple, Samsung, Omachi, ...)
        /// </summary>
        public string BrandName { get; set; }


        public DateTime DateCreated { get; set; }

        public int Status { get; set; }

        /// <summary>
        /// URL của ảnh sản phẩm
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Mô tả ngắn gọn
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Mã cửa hàng.
        /// </summary>
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