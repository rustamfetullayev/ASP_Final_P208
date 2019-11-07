using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarShop.Models;

namespace CarShop.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        private readonly CarShopEntities1 db = new CarShopEntities1();
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            if (Session["adminLog"] == null)
            {
                return RedirectToAction("Index", "AdminAccount");
            }
            return View();
        }
    }
}