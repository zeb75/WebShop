using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult ProductList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return Json(db.Products.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Productdetail(Product product)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            Product OldProduct = db.Products.SingleOrDefault(p => p.Id == product.Id);

            OldProduct.Brand = product.Brand;
            OldProduct.Model = product.Model;
            OldProduct.Description = product.Description;
            OldProduct.Price = product.Price;
            OldProduct.Image = product.Image;

            db.SaveChanges();
            return Json(product, JsonRequestBehavior.AllowGet);
        }
    }
}