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
        private Customer _customer;
        private int _totalAmount;
        private int _totalValue;

        /// <summary>
        /// Mã hóa đơn.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Chỉ dùng trong trường hợp khách hàng có thẻ thành viên, để tích lũy điểm thưởng. Nếu không, trường này sẽ là NULL.
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
        /// Tổng giá trị hóa đơn bán.
        /// </summary>
        public int TotalValue
        {
            get
            {
                int sum = 0;
                foreach (var item in DetailSaleBills)
                {
                    sum += (item.Amount * item.Product.Price);
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

        public virtual ICollection<DetailSaleBill> DetailSaleBills { get; set; }
    }
}