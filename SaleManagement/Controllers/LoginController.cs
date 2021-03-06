﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleManagement.Models;
using SaleManagement.Models.DAL;
using SaleManagement.Controllers.Utilities;
using SaleManagement.Controllers.Session;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SaleManagement.Controllers
{
    public class LoginController : Controller
    {
        public ApplicationDbContext _context = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View("~/Views/admin_1_rounded/page_user_login_1.cshtml");
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string role)
        {
            if (role == "Admin" || role == "admin")
            {
                Admin admin = _context.Admin.SingleOrDefault(m => m.UserName == username);
                if (admin != null) // Nếu tồn tại tài khoản.
                {
                    string passwordHashed = EncryptionHelper.GetHash(password + admin.Salt); //Kiểm tra password.
                    if (admin.PasswordEncrypted == passwordHashed) //Đăng nhập thành công !
                    {
                        Session[SessionKey.UserName] = admin.UserName;
                        Session[SessionKey.StoreID] = 0;
                        return View("~/Views/admin_1_rounded/dashboard_2.cshtml"); // Đưa về trang màn hình chính.
                    }
                    else
                    {
                        ViewBag.Result = "Wrong Password";
                        return View("~/Views/admin_1_rounded/page_user_login_1.cshtml");
                    }
                }
                else
                {
                    ViewBag.Result = "UserName Not Exists";
                    return View("~/Views/admin_1_rounded/page_user_login_1.cshtml");
                }
            }
            else
            {
                Staff staff = _context.Staff.SingleOrDefault(m => m.Code == username);
                if (staff != null) // Nếu tồn tại tài khoản.
                {
                    string passwordHashed = EncryptionHelper.GetHash(password + staff.Salt); //Kiểm tra password.
                    if (staff.PasswordEncrypted == passwordHashed)
                    {
                        Session[SessionKey.StoreID] = 0;
                        return View("~/Views/admin_1_rounded/dashboard_2.cshtml"); // Đưa về trang màn hình chính.
                    }
                    else
                    {
                        ViewBag.Result = "Wrong Password";
                        return View("~/Views/admin_1_rounded/page_user_login_1.cshtml");
                    }
                }
                else
                {
                    ViewBag.Result = "UserName Not Exists";
                    return View("~/Views/admin_1_rounded/page_user_login_1.cshtml");
                }
            }
        }

        public ActionResult GetUserName_JSON()
        {            
            string converted = JsonConvert.SerializeObject(
                Session[SessionKey.UserName].ToString(),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }

        public ActionResult GetLoginInfo_JSON()
        {
            string userName = Session[SessionKey.UserName].ToString();
            string converted = JsonConvert.SerializeObject(
                _context.Admin.SingleOrDefault(m => m.UserName == userName),
                Formatting.None,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
            return Content(converted, "application/json");
        }
    }
}