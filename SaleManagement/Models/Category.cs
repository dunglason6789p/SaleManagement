using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleManagement.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        [Display(Name = "Miêu tả")]
        public string Description { get; set; }

        [Display(Name = "Mã danh mục cha")]
        public int ParentCategoryID { get; set; }
        [NotMapped]
        public Category ParentCategory
        {
            get => _parentCategory; set
            {
                _parentCategory = value;
                ParentCategoryID = value.ID;
            }
        }

        /// <summary>
        /// Mã cửa hàng.
        /// </summary>
        [Display(Name = "Mã cửa hàng")]
        public int StoreID { get; set; }
        [NotMapped]
        private Store _store;
        [NotMapped]
        private Category _parentCategory;

        [NotMapped]
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