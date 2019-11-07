using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarShop.Models;

namespace CarShop.Viewmodels
{
    public class SingleCarVM
    {
        public CarAnnouncement Car { get; set; }
        public IEnumerable<CarAnnouncement> Announcements { get; set; }
        public IEnumerable<News> News { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}