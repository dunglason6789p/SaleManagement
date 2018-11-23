using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleManagement.Models;
using SaleManagement.Models.DAL;
using SaleManagement.Controllers.Utilities;

namespace SaleManagement.Controllers
{
    public class LoginController : Controller
    {
        public ApplicationDbContext _context = new ApplicationDbContext();

        [HttpGet]
        public ActionResult LoginOLD()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult LoginOLD(String username, String PasswordEncrypted)
        {
            Debug.WriteLine(username + ":::" + PasswordEncrypted);
            if(_context.Account.Any(m => m.UserName == username && m.PasswordEncrypted == PasswordEncrypted))
            {
                return View("~/Views/Home/Index.cshtml");
            } else
            {
                ViewBag.Result = "Login fail";
                return View();
            }
            
        }




        
        

        [HttpGet]
        public ActionResult Login()
        {
            return View("~/Views/DangNhap/DangNhap.cshtml");
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string role)
        {
            Debug.WriteLine(username + ":::" + password);
            Account account = _context.Account.SingleOrDefault(m => m.UserName == username);
            if (account != null)
            {
                string passwordHashed = EncryptionHelper.GetHash(password + account.Salt);
                if (account.PasswordEncrypted == passwordHashed)
                {
                    return View("~/Views/Home/Index.cshtml");
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