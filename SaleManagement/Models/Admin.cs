using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleManagement.Models
{
    public class Admin : Account
    {
        #region Account
        /// <summary>
        /// ID của tài khoản. Cái này được tạo tự động trong database (auto-increment). Cái này không phải UserName, không dùng để đăng nhập.
        /// </summary>
        [Key]
        public int ID { get; set; } // KHÔNG PHẢI UserName. Không dùng để đăng nhập.

        /// <summary>
        /// UserName. Dùng để đăng nhập.
        /// </summary>   
        [StringLength(450)]
        [Index(IsUnique = true)] // UNIQUE CONSTRAINT.
        public string UserName { get; set; }

        /// <summary>
        /// Mật khẩu lưu trong database (ở dạng đã mã hóa bằng cách hash(password + salt).
        /// </summary>
        public string PasswordEncrypted { get; set; }

        public string Salt { get; set; }

        /// <summary>
        /// 1=active, 0=disabled
        /// </summary>
        public int Status { get; set; }
        #endregion

        /// <summary>
        /// Một tài khoản Admin có thể sở hữu nhiều cửa hàng.
        /// </summary>
        public virtual ICollection<Store> Stores { get; set; }
    }
}