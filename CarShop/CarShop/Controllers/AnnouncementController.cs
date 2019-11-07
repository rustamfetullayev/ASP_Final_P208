using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarShop.Models;
using System.IO;
using CarShop.Extensions;
using System.Threading;
using System.Data.Entity;
using CarShop.Viewmodels;

namespace CarShop.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly CarShopEntities1 db = new CarShopEntities1();
        // GET: Announcement
        [AuthorizeUser]
        public ActionResult Create()
        {
            ViewBag.Marks = new SelectList(db.Marks, "ID", "Name");
            ViewBag.Models = new SelectList(db.Models, "ID", "Name");
            ViewBag.Colors= new SelectList(db.Colors, "ID", "Name");
            ViewBag.Cities= new SelectList(db.Cities, "ID", "Name");
            ViewBag.Fuels= new SelectList(db.Fuels, "ID", "Name");
            ViewBag.Gears= new SelectList(db.Gearboxes, "ID", "Name");

            return View();
        }
        [AuthorizeUser]
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Image")]CarAnnouncement car,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Extension.CheckImageType(Image))
                {
                    User currusr = Session["userLog"] as User;
                    car.Image = Extension.SaveImage(Server.MapPath("~/Source/images"), Image);
                    car.PostDate = DateTime.Now;
                    car.UpdateDate = DateTime.Now;
                    car.UserID = currusr.ID;
                    db.CarAnnouncements.Add(car);
                    db.SaveChanges();

                    return RedirectToAction("Personal","Account",new { id=currusr.ID});
                }
                else
                {
                    ModelState.AddModelError("Image", "Type of picture is not valid.");
                }
            }
            ViewBag.Marks = new SelectList(db.Marks, "ID", "Name");
            ViewBag.Models = new SelectList(db.Models, "ID", "Name");
            ViewBag.Colors = new SelectList(db.Colors, "ID", "Name");
            ViewBag.Cities = new SelectList(db.Cities, "ID", "Name");
            ViewBag.Fuels = new SelectList(db.Fuels, "ID", "Name");
            ViewBag.Gears = new SelectList(db.Gearboxes, "ID", "Name");
            return View();
        }

        public ActionResult LoadModels(int markId)
        {
            return Json(db.Models.Where(m => m.MarkID == markId).Select(m=>new { m.ID,m.Name}), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadAnnouncement(int skip)
        {
            IEnumerable<CarAnnouncement> vipcars = db.CarAnnouncements.Where(a => a.IsVIP == true).OrderByDescending(a => a.UpdateDate);
            IEnumerable<CarAnnouncement> basiccars = db.CarAnnouncements.Where(a => a.IsVIP == false).OrderByDescending(a => a.UpdateDate);
            IEnumerable<CarAnnouncement> unionlist = vipcars.Union(basiccars);
            return PartialView("_PartialAnnouncements", unionlist.Skip(skip).Take(3));
        }

        [AuthorizeUser]
        public ActionResult Delete(int? id)
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

        [AuthorizeUser]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmCar(int id)
        {
            CarAnnouncement car = db.CarAnnouncements.Find(id);

            int? userid = car.UserID;

            if (!Extension.DeleteImage(Server.MapPath("~/Source/images"), car.Image))
            {
                ViewBag.DeleteError = "File doesn't exist";
                return View();
            }

            db.CarAnnouncements.Remove(car);
            db.SaveChanges();

            return RedirectToAction("Personal", "Account", new { id = userid });
        }

        [HttpPost]
        public ActionResult Search(string query)
        {
            Viewmodel vm = new Viewmodel
            {
                Cars = db.CarAnnouncements.Where(p => p.Title.ToLower().Contains(query.ToLower()) || p.About.ToLower().Contains(query.ToLower()) || p.Model.Name.ToLower().Contains(query.ToLower()) || p.Model.Mark.Name.ToLower().Contains(query.ToLower())).ToList(),
                News = db.News.OrderByDescending(n => n.PostDate).Take(5),
                MainNews = null,
                CarCount = 0
            };

            return View(vm);
        }
    }
}