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

        //Unique check is done only in application layer (unique by each store).
        public string Code { get; set; }

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
                if (value != null) CustomerID = value.ID;
            }
        }

        public int StaffID { get; set; }
        [NotMapped]
        private Staff _staff;
        [NotMapped]
        public Staff Staff
        {
            get => _staff; set
            {
                _staff = value;
                if (value != null) StaffID = value.ID;
            }
        }

        /// <summary>
        /// Biến private cho property TotalValue.
        /// </summary>
        /// <summary>
        /// Tổng giá trị hóa đơn bán.
        /// </summary>
        public int TotalValue { get; set; }

        public int PaymentBank { get; set; }

        public int PaymentCash { get; set; }

        public int GetPaymentTotal() { return PaymentBank + PaymentCash; }

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
        [NotMapped]
        private Store _store;
        [NotMapped]
        public virtual Store Store
        {
            get => _store; set
            {
                _store = value;
                if (value != null) StoreID = value.ID; //Để cho khi gán object thì gán luôn cả ID (khóa ngoại).
            }
        }

        [NotMapped]
        public virtual ICollection<SaleBillDetail> SaleBillDetails { get; set; }

        public void RefreshTotalValue()
        {
            TotalValue = GetTotalValue();
        }

        public int GetTotalValue()
        {
            int sum = 0;
            if (SaleBillDetails != null)
                foreach (var item in SaleBillDetails)
                {
                    if (item != null) sum += item.Amount * item.Price;
                    else SaleBillDetails.Remove(item);
                }
            return sum;
        }
    }
}