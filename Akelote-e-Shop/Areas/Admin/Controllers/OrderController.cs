using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Akelote_e_Shop.Areas.Admin.Services;
using Akelote_e_Shop.Areas.Admin.Services.DI;
using Akelote_e_Shop.Models;

namespace Akelote_e_Shop.Areas.Admin.Controllers
{
    public class OrderController : Controller {

        private readonly ImportExportService _importExportService;
        private ApplicationDbContext db = new ApplicationDbContext();

        public OrderController() {
            _importExportService = new ExcelImportExportService();
        }

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
        [HttpPost, ActionName("Complete")]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteConfirmed(int id) {
            Order order = db.Order.Find(id);
            order.Status = OrderStatus.Completed;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null) {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Order/Export
        public void Export() {
            _importExportService.Export(db.Order.ToList(), "Orders");
        }
    }
}