using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarShop.Models;
using CarShop.Viewmodels;

namespace CarShop.Controllers
{
    public class NewsController : Controller
    {
        CarShopEntities1 db = new CarShopEntities1();
        // GET: News
        public ActionResult Index()
        {
            Viewmodel vm = new Viewmodel
            {
                Cars = null,
                News = db.News.OrderByDescending(n => n.PostDate).Take(5),
                MainNews=db.News.OrderByDescending(n=>n.PostDate)
            };

            return View(vm);
        }

        public ActionResult Single(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            News news = db.News.Find(id);

            if (news == null)
            {
                return HttpNotFound();
            }

            SingleNewsVM vm = new SingleNewsVM
            {
                News = news,
                RelatedNews = db.News.Where(n => n.Title.Contains(news.Title)).Take(3),
                NewsSide = db.News.OrderByDescending(n => n.PostDate).Take(5)
            };

            return View(vm);
        }
    }
}