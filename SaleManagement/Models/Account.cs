using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaleManagement.Models
{
    public class Account
    {
        private Customer _customer;

        [Key]
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

        /// <summary>
        /// Trường CustomerID trong Database. Nếu tài khoản không phải là 1 Customer, thì trường này NULL.
        /// </summary>
        public int CustomerID { get; set; }
        public Customer Customer
        {
            get => _customer; set
            {
                _customer = value;
                CustomerID = value.ID;
            }
        }

        /// <summary>
        /// 0=Customer; 1=Staff; 2=Admin;
        /// </summary>
        public int RoleCode { get; set; }
    }
}