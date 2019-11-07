using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarShop.Models;

namespace CarShop.Viewmodels
{
    public class SingleNewsVM
    {
        public News News { get; set; }
        public IEnumerable<News> RelatedNews { get; set; }
        public IEnumerable<News> NewsSide { get; set; }
    }
}