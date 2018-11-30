using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleManagement.Models
{
    public class SaleBill
    {
        
        
        

        /// <summary>
        /// Mã hóa đơn.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Chỉ dùng trong trường hợp khách hàng có thẻ thành viên, để tích lũy điểm thưởng. Nếu không, trường này sẽ là NULL.
        /// </summary>
        public int CustomerID { get; set; }
        private Customer _customer;
        [NotMapped]
        public Customer Customer
        {
            get => _customer; set
            {
                _customer = value;
                CustomerID = value.ID;
            }
        }

        /// <summary>
        /// Biến private cho property TotalValue.
        /// </summary>
        private int _totalValue;
        /// <summary>
        /// Tổng giá trị hóa đơn bán.
        /// </summary>
        public int TotalValue
        {
            get
            {
                int sum = 0;
                foreach (var item in SaleBillDetails)
                {
                    sum += (item.Amount * item.Price);
                }
                return sum;
            }
            private set { }
        }
        

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Discount by entering coupon code.
        /// </summary>
        public int DiscountValue { get; set; }

        /// <summary>
        /// Mã cửa hàng.
        /// </summary>
        public int StoreID { get; set; }
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
        
        [NotMapped]
        public virtual ICollection<SaleBillDetail> SaleBillDetails { get; set; }
    }
}