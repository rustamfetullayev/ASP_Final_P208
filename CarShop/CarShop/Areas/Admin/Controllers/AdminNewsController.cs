using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarShop.Models;
using CarShop.Extensions;
using System.Data.Entity;

namespace CarShop.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class AdminNewsController : Controller
    {
        private readonly CarShopEntities1 db = new CarShopEntities1();
        // GET: Admin/News
        public ActionResult Index()
        {
            return View(db.News.OrderByDescending(a=>a.PostDate));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Image")] News news, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Extension.CheckImageType(Image))
                {
                    news.Image = Extension.SaveImage(Server.MapPath("~/Source/images"),Image);
                    news.PostDate = DateTime.Now;
                    db.News.Add(news);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Image", "Type of picture is not valid.");
                }
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            News news = db.News.Find(id);

            if (news == null)
            {
                return HttpNotFound();
            }

            return View(news);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            News news = db.News.Find(id);

            if (!Extension.DeleteImage(Server.MapPath("~/Source/images"), news.Image))
            {
                ViewBag.DeleteError = "File doesn't exist";
                return View();
            }

            db.News.Remove(news);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            News news = db.News.Find(id);

            if (news == null)
            {
                return HttpNotFound();
            }

            return View(news);
        }

        [HttpPost]
        public ActionResult Edit(News news, HttpPostedFileBase NewImage)
        {
            if (ModelState.IsValid)
            {
                if (NewImage != null)
                {
                    Extension.DeleteImage(Server.MapPath("~/Source/images"), news.Image);

                    news.Image = Extension.SaveImage(Server.MapPath("~/Source/images"),NewImage);
                }

                news.PostDate = DateTime.Now;
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}