using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarShop.Models;

namespace CarShop.Viewmodels
{
    public class Viewmodel
    {
        public int CarCount { get; set; }
        public IEnumerable<CarAnnouncement> Cars { get; set; }
        public IEnumerable<News> News { get; set; }
        public IEnumerable<News> MainNews { get; set; }
    }
}