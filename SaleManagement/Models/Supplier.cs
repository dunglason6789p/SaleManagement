using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleManagement.Models
{
    public class Supplier
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Phải có storeID là vì ta còn tính nợ cho supplier !
        /// </summary>
        public int StoreID { get; set; }
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

        /// <summary>
        /// Nếu &lt; 0 thì cửa hàng của ta đang nợ nhà cung cấp này. Nhớ là supplier.Money == importBill.Payment - importBill.TotalValue
        /// </summary>
        public int Money { get; set; }
    }
}