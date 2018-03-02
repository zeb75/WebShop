using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include("OrderRows").SingleOrDefault(x => x.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult AddProduct(int oId)
        {
            List<Product> products = db.Products.ToList();
            ViewBag.oId = oId;
            return View(products);
        }
        [HttpGet]
        public ActionResult ProductToOrderRow(int pId, int oId)
        {

            Order order = db.Orders.Include("OrderRows").Include("OrderRows.Products").SingleOrDefault(o => o.Id == oId);

            bool foundIt = false;

            foreach (var item in order.OrderRows)
            {
                if (item.Product.Id == pId)
                {
                    item.Amount++;
                    foundIt = true;
                    break;
                }
            }

            if (foundIt == false)
            {
                Product product = db.Products.SingleOrDefault(p => p.Id == pId);

                OrderRow orderRow = new OrderRow();

                orderRow.Amount = 1;
                orderRow.Product = product;
                orderRow.Price = product.Price;
                order.OrderRows.Add(orderRow);
            }


            db.SaveChanges();

            return RedirectToAction("Details", new { id = oId });
        }

        [HttpGet]
        public ActionResult RemoveProduct(int oId)
        {
            List<Product> products = db.Products.ToList();
            ViewBag.oId = oId;
            return View(products);
        }
        [HttpGet]
        public ActionResult RemoveProductFromOrderRow(int pId, int oId)
        {
            Order order = db.Orders.Include("OrderRows").Include("OrderRows.Product").SingleOrDefault(o => o.Id == oId);


            foreach (var item in order.OrderRows)
            {
                if (item.Product.Id == pId)
                {
                    item.Amount--;
                    if (item.Amount == 0)
                    {
                        order.OrderRows.Remove(item);
                    }
                    break;
                }
            }


            db.SaveChanges();

            return RedirectToAction("Details", new { id = oId });
        }

        public ActionResult OrderHistory()
        
            {
                var uId = User.Identity.GetUserId();
                ApplicationDbContext db = new ApplicationDbContext();
                ApplicationUser applicationUser = db.Users.Include("Orders").SingleOrDefault(u => u.Id == uId);


                return View(applicationUser.Orders);

            }

        public ActionResult OrderHistoryDetails(int oId)
        {
            var order = db.Orders.SingleOrDefault(o => o.Id == oId);
            return View(order);
        }



    }
}
