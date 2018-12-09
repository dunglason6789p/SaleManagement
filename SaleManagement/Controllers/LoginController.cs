using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleManagement.Models;
using SaleManagement.Models.DAL;
using SaleManagement.Controllers.Utilities;
using SaleManagement.Controllers.Session;

namespace SaleManagement.Controllers
{
    public class LoginController : Controller
    {
        public ApplicationDbContext _context = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View("~/Views/DangNhap/DangNhap.cshtml");
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string role)
        {
            Debug.WriteLine(username + ":::" + password);
            if (role == "Admin" || role == "admin")
            {
                Admin admin = _context.Admin.SingleOrDefault(m => m.UserName == username);
                if (admin != null) // Nếu tồn tại tài khoản.
                {
                    string passwordHashed = EncryptionHelper.GetHash(password + admin.Salt); //Kiểm tra password.
                    if (admin.PasswordEncrypted == passwordHashed) //Đăng nhập thành công !
                    {
                        Session[SessionKey.UserName] = admin.UserName;
                        return View("~/Views/Home/Index.cshtml"); // Đưa về trang màn hình chính.
                    }
                    else
                    {
                        ViewBag.Result = "WrongPassword";
                        return View("~/Views/Login/Login.cshtml");
                    }
                }
                else
                {
                    ViewBag.Result = "UserNameNotExists";
                    return View("~/Views/Login/Login.cshtml");
                }
            }
            else
            {
                Staff staff = _context.Staff.SingleOrDefault(m => m.UserName == username);
                if (staff != null) // Nếu tồn tại tài khoản.
                {
                    string passwordHashed = EncryptionHelper.GetHash(password + staff.Salt); //Kiểm tra password.
                    if (staff.PasswordEncrypted == passwordHashed)
                    {
                        return View("~/Views/Home/Index.cshtml"); // Đưa về trang màn hình chính.
                    }
                    else
                    {
                        ViewBag.Result = "WrongPassword";
                        return View("~/Views/Login/Login.cshtml");
                    }
                }
                else
                {
                    ViewBag.Result = "UserNameNotExists";
                    return View("~/Views/Login/Login.cshtml");
                }
            }
        }



    }
}