using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Akelote_e_Shop.Models;

namespace Akelote_e_Shop.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Order
        public ActionResult Index() {
            var order = db.Order.Include(c => c.OrderItems);
            return View(order.ToList());
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null) {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/Order/Complete/5
        public ActionResult Complete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null) {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Complete/5
        [HttpPost, ActionName("Put")]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteConfirmed(int id) {
            Order order = db.Order.Find(id);
            order.Status = OrderStatus.Completed;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Order/Cancel/5
        public ActionResult Cancel(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null) {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Cancel/5
        [HttpPost, ActionName("Put")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelConfirmed(int id) {
            Order order = db.Order.Find(id);
            order.Status = OrderStatus.Cancelled;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}