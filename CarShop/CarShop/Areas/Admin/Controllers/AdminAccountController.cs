using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarShop.Models;
using System.Web.Helpers;

namespace CarShop.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly CarShopEntities1 db = new CarShopEntities1();
        // GET: Admin/AdminAccount
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.LoginError = "Please fill empty areas correctly.";
                return View();
            }

            CarShop.Models.Admin admin = db.Admins.FirstOrDefault(s => s.Username==username);

            if (admin == null)
            {
                ViewBag.LoginError = "Username or password is wrong.";
                return View();
            }

            if (!Crypto.VerifyHashedPassword(admin.Password, password))
            {
                ViewBag.LoginError = "Username or password is wrong.";
                return View();
            }

            Session["adminLog"] = admin;
            return RedirectToAction("Index", "AdminHome");
        }

        public ActionResult Logout()
        {
            Session["adminLog"] = null;

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}