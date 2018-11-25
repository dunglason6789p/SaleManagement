using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SaleManagement.Models
{
    /// <summary>
    /// Thông tin khách hàng. Khách hàng sẽ không có tài khoản để đăng nhập vào hệ thống, mà chỉ có thông tin lưu trên hệ thống. Chỉ những
    /// khách hàng muốn lưu điểm tích lũy, hoặc muốn nợ tiền hàng, thì mới yêu cầu lưu thông tin.
    /// </summary>
    public class Customer : Person //Kế thừa từ lớp Person.
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Số điểm trong thẻ tích điểm.
        /// </summary>
        public int Point { get; set; }

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