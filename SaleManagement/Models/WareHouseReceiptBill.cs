using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaleManagement.Models
{
    /// <summary>
    /// Hóa đơn nhập hàng.
    /// </summary>
    public class WareHouseReceiptBill
    {
        private Supplier _supplier;
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
        public int SupplierID { get; set; }
        public Supplier Supplier
        {
            get => _supplier; set
            {
                _supplier = value;
                SupplierID = value.ID;
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