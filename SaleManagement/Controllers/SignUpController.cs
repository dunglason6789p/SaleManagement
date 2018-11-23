using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleManagement.Models;
using SaleManagement.Controllers.Utilities;
using SaleManagement.Models.DAL;

namespace SaleManagement.Controllers
{
    public class SignUpController : Controller
    {
        public ApplicationDbContext _context = new ApplicationDbContext();

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(string name, string email, string username, string password, string confirmpassword)
        {
            if (password != confirmpassword)
            {
                ViewBag.Result = "WrongConfirmpassword";
                return View();
            }
            if (_context.Account.Any(m => m.UserName == username))
            {
                ViewBag.Result = "UserNameExisted";
                return View();
            }
            _context.Account.Add(new Account() {
                UserName = username,
                PasswordEncrypted = EncryptionHelper.GetHash(password + EncryptionHelper.GetSalt()),
                Status = 0,
                CustomerID = 0,
                RoleCode = 0,
            });

            ViewBag.Result = "Success";
            return View();
        }
    }
}