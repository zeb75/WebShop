﻿using System;
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
        private ApplicationDbContext db = new ApplicationDbContext();


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
        public ActionResult EditCart(int id, string edit)
        {
            var uId = User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser applicationUser = db.Users.SingleOrDefault(u => u.Id == uId);

            foreach (var item in applicationUser.CartItems)
            {
                if (item.Id == id)
                {

                    if(edit == "plus")
                    {
                        item.Amount++;
                    }

                    else if(item.Amount > 1)
                    {
                        item.Amount--;
                    }
                  
                    else
                    {
                        applicationUser.CartItems.Remove(item);
                    }
                    break;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Cart");
        }

        [Authorize]
        public ActionResult Checkout()
        {
            var uId = User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser applicationUser = db.Users.Include("CartItems.Product").Include("Orders").SingleOrDefault(u => u.Id == uId);


            if (applicationUser.CartItems.Count > 0)
            {
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

            else
            {
               return RedirectToAction("Cart");
            }

        }
       
        public JsonResult IsLoggedIn()

        {
            if(User.Identity.IsAuthenticated)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult OrderHistory()

        {
            var uId = User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser applicationUser = db.Users.Include("Orders").SingleOrDefault(u => u.Id == uId);

            applicationUser.Orders = applicationUser.Orders.OrderByDescending(o => o.OrderDate).ToList();  /* Sortera lista*/

            return View(applicationUser.Orders);


        }

        public ActionResult OrderHistoryDetails(int oId)
        {
            var order = db.Orders.SingleOrDefault(o => o.Id == oId);
            return View(order);
        }

    }
}