using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleManagement.Models.DAL;

namespace SaleManagement.Controllers
{
    public class LoginController : Controller
    {
        public ApplicationDbContext _context = new ApplicationDbContext();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(String username, String PasswordEncrypted)
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
    }
}