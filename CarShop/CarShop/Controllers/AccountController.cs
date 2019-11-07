using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using CarShop.Models;
using CarShop.Viewmodels;

namespace CarShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly CarShopEntities1 db = new CarShopEntities1();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email,string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.LoginError = "Please fill empty areas correctly.";
                return View();
            }

            User currentuser = db.Users.FirstOrDefault(u => u.Email == email);

            if (currentuser == null)
            {
                ViewBag.LoginError = "Username or password is wrong.";
                return View();
            }

            if (!Crypto.VerifyHashedPassword(currentuser.Password, password))
            {
                ViewBag.LoginError = "Username or password is wrong.";
                return View();
            }

            Session["userLog"] = currentuser;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session["userLog"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string name,string surname,string email,string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
            {
                ViewBag.LoginError = "Please fill empty areas correctly.";
                return View();
            }

            if (password.Length<6)
            {
                ViewBag.LoginError = "Please select strong password.";
                return View();
            }

            User usr = db.Users.FirstOrDefault(u => u.Email == email);

            if (usr != null)
            {
                ViewBag.LoginError = "Please select another email.";
                return View();
            }

            User newusr = new User
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = Crypto.HashPassword(password)
            };

            db.Users.Add(newusr);
            db.SaveChanges();

            Session["userLog"] = newusr;

            return RedirectToAction("Index","Home");
        }

        public ActionResult Personal(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            User usr = db.Users.Find(id);

            if (usr == null)
            {
                return HttpNotFound();
            }

            SingleUserVM vm = new SingleUserVM
            {
                User = usr,
                Cars = db.CarAnnouncements.Where(a => a.UserID == usr.ID).OrderByDescending(a=>a.UpdateDate),
                News = db.News.OrderByDescending(n => n.PostDate).Take(5),
            };

            return View(vm);
        }
    }
}