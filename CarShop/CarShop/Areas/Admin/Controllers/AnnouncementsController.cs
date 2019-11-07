using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarShop.Models;

namespace CarShop.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class AnnouncementsController : Controller
    {
        private readonly CarShopEntities1 db = new CarShopEntities1();
        // GET: Admin/Announcements
        public ActionResult Index()
        {
            return View(db.CarAnnouncements.OrderByDescending(a=>a.UpdateDate));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            CarAnnouncement car = db.CarAnnouncements.Find(id);

            if (car == null)
            {
                return HttpNotFound();
            }

            return View(car);
        }
    }
}