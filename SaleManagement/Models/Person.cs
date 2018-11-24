using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleManagement.Models
{
    public abstract class Person
    {
        //Các trường private (tạo tự động).
        private string _lastName;
        private string _familyName;
        private string _fullName;
        
        /// <summary>
        /// 0=nữ, 1=nam
        /// </summary>
        public bool Gender { get; set; }

        public DateTime BirthDay { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        /// <summary>
        /// Tên họ+đệm, VD : "Nguyễn Trường" trong "Nguyễn Trường Sơn".
        /// </summary>
        public string FamilyName
        {
            get => _familyName;
            set
            {
                _familyName = Regex.Replace(value, @"\s+", " ").Trim();
                if (!String.IsNullOrEmpty(_lastName))
                {
                    _fullName = _familyName + " " + _lastName;
                }
                else
                {
                    _fullName = _familyName;
                }
            }
        }

        /// <summary>
        /// Tên gọi, VD : "Sơn" trong "Nguyễn Trường Sơn".
        /// </summary>
        [Required]
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = Regex.Replace(value, @"\s+", " ").Trim();
                if (!String.IsNullOrEmpty(_familyName))
                {
                    _fullName = _familyName + " " + _lastName;
                }
                else
                {
                    _fullName = _lastName;
                }
            }
        }

        /// <summary>
        /// Tên đầy đủ. VD : Nguyễn Trường Sơn.
        /// </summary>
        [Required]
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = Regex.Replace(value, @"\s+", " ").Trim();
                if (_fullName.Contains(" "))
                {
                    _lastName = GetLastName(_fullName);
                    _familyName = GetFamilyName(_fullName);
                }
                else
                {
                    _lastName = _fullName;
                    _familyName = "";
                }
            }
        }

        /// <summary>
        /// Tìm kí tự space cuối cùng xuất hiện trong string. Dùng để tách tên khỏi FullName.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int FindLastWhiteSpacePosition(string str)
        {
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (str[i].Equals(' '))
                {
                    return i;
                }
            }
            return 0;
        }
        private string GetLastName(string str)
        {
            return (str.Substring(FindLastWhiteSpacePosition(str) + 1));
        }
        private string GetFamilyName(string str)
        {
            return (str.Substring(0, FindLastWhiteSpacePosition(str)));
        }
    }
}