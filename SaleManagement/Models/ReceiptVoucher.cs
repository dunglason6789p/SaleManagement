using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleManagement.Models
{
    public class ReceiptVoucher
    {
        [Key]
        public int ID { get; set; }

        public int StaffID { get; set; }
        private Staff _staff;
        [ForeignKey("StaffID")]
        public Staff Staff
        {
            get => _staff; set
            {
                _staff = value;
                StaffID = value.ID;
            }
        }

        //Cho phép khách hàng nợ tiền mua hàng, nhưng yêu cầu khách phải có thông tin cá nhân đăng ký với cửa hàng.
        public int CustomerID { get; set; }
        private Customer _customer;
        [ForeignKey("CustomerID")]
        public Customer Customer
        {
            get => _customer; set
            {
                _customer = value;
                CustomerID = value.ID;
            }
        }

        public DateTime DateCreated { get; set; }

        public int Value { get; set; }

        public string Description { get; set; }

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