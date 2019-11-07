using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarShop.Models;
using CarShop.Viewmodels;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarShopEntities1 db=new CarShopEntities1();

        public ActionResult Index()
        {
            IEnumerable<CarAnnouncement> vipcars = db.CarAnnouncements.Where(a => a.IsVIP == true).OrderByDescending(a => a.UpdateDate);
            IEnumerable<CarAnnouncement> basiccars = db.CarAnnouncements.Where(a => a.IsVIP == false).OrderByDescending(a => a.UpdateDate);
            IEnumerable<CarAnnouncement> unionlist = vipcars.Union(basiccars);

            Viewmodel vm = new Viewmodel
            {
                CarCount=db.CarAnnouncements.Count(),
                Cars = unionlist.Take(3),
                News = db.News.OrderByDescending(n=>n.PostDate).Take(5),
                MainNews=null
            };

            return View(vm);
        }

        public ActionResult Single(int? id)
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

            SingleCarVM vm = new SingleCarVM
            {
                Car = car,
                Announcements = db.CarAnnouncements.Where(a => a.Model.Mark.Name == car.Model.Mark.Name).Take(3),
                News = db.News.OrderByDescending(n => n.PostDate).Take(5),
                Comments=db.Comments.Where(c=>c.CarID==id).OrderByDescending(c=>c.ComDate)
            };

            return View(vm);
        }

        public ActionResult AddComment(string text, int carid)
        {
            User currentUser = Session["userLog"] as User;
            Comment newcom = new Comment
            {
                UserID = currentUser.ID,
                CarID = carid,
                Content = text,
                ComDate = DateTime.Now,
            };

            newcom = db.Comments.Add(newcom);
            db.SaveChanges();
            newcom.User = currentUser;
            return PartialView("_PartialComment", newcom);
        }
    }
}