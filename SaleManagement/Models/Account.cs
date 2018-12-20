using System.ComponentModel.DataAnnotations;

namespace SaleManagement.Models
{
    /// <summary>
    /// Đây là 1 Interface. Lớp Admin và Staff sẽ implement interface này.
    /// </summary>
    public interface Account
    {
        /// <summary>
        /// ID của tài khoản. Cái này được tạo tự động trong database (auto-increment). Cái này không phải UserName, không dùng để đăng nhập.
        /// </summary>
        int ID { get; set; } // KHÔNG PHẢI UserName. Không dùng để đăng nhập. 

        /// <summary>
        /// UserName. Dùng để đăng nhập.
        /// </summary>        
        [Display(Name = "Tên đăng nhập")]
        string UserName { get; set; }

        /// <summary>
        /// Mật khẩu lưu trong database (ở dạng đã mã hóa bằng cách hash(password + salt).
        /// </summary>
        string PasswordEncrypted { get; set; }

        string Salt { get; set; }

        /// <summary>
        /// 1=active, 0=disabled
        /// </summary>
        [Display(Name = "Trạngt thái")]
        int Status { get; set; }
    }
}