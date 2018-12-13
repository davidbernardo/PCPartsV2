using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PCPartsV2.Models;
using System.Collections.Generic;
using System;
using System.Collections;

namespace PCPartsV2.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Most recent products && Most recent and most discounted products
        public ActionResult Index()
        {
            var recent = db.Products.Include(p => p.ProductType).OrderByDescending(x => x.ProductID).Take(4).ToList();
            var discounted = db.Products.Include(p => p.ProductType).Where(p => p.Discount > 0).OrderByDescending(x => x.ProductID).ThenByDescending(c => c.Discount).Take(4).ToList();
            ViewBag.DiscountedProducts = discounted;
            ViewBag.RecentProducts = recent;

            return View();
        }
        
        //GET: About view
        public ActionResult About()
        {
            return View();
        }

    }
}