using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SaleManagement.Models
{
    public class Customer : Person
    {
        /// <summary>
        /// Số điểm trong thẻ tích điểm.
        /// </summary>
        public int Point { get; set; }
    }
}