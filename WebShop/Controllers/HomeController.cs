using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Campaign()
        {
            return View();
        }

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

        [Authorize]
        public ActionResult Cart(Product product)

        {
            var uId = User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser applicationUser = db.Users.SingleOrDefault(u => u.Id == uId);

            foreach (var item in applicationUser.CartItems)
            {
                item.Product = db.Products.SingleOrDefault(p => p.Id == item.ProductRefId);
            }
            return View(applicationUser.CartItems);

        }

        public JsonResult ProductList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return Json(db.Products.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProductDetail(Product product)
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


        [Authorize]
        public JsonResult AddToCart(Product product)
        {
            var uId = User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser applicationUser = db.Users.SingleOrDefault(u => u.Id == uId);
            bool notFound = true;

            foreach (var item in applicationUser.CartItems)
            {
                if(item.ProductRefId == product.Id)
                {
                    item.Amount++;
                    notFound = false;
                    break;
                }
            }

            if(notFound)
            {
                CartItem cartItem = new CartItem() { Amount = 1, ProductRefId = product.Id};
                applicationUser.CartItems.Add(cartItem);
            }
            db.SaveChanges();
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult Checkout()
        {
            var uId = User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser applicationUser = db.Users.Include("CartItems.Product").Include("Orders").SingleOrDefault(u => u.Id == uId);

            Order order = new Order();
            order.OrderDate = DateTime.Now;
            

            foreach (var item in applicationUser.CartItems)
            {
                OrderRow orderrow = new OrderRow();
                orderrow.Amount = item.Amount;
                orderrow.Product = item.Product;
                orderrow.Price = item.Product.Price;

                order.OrderRows.Add(orderrow);
            }


            applicationUser.Orders.Add(order);
            applicationUser.CartItems.Clear();

            db.SaveChanges();

            return View(order);

        }
       
    }
}