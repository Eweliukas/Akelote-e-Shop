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
using Microsoft.VisualBasic.ApplicationServices;

namespace Akelote_e_Shop.Areas.Admin.Controllers {
    public class UserController : Controller {
        private readonly ImportExportService _importExportService = new ExcelImportExportService();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/User
        public ActionResult Index() {
            var user = db.Users.Include(c => c.Orders);
            return View(user.ToList());
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(string id) {
            if (String.IsNullOrEmpty(id)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null) {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(string id) {
            if (String.IsNullOrEmpty(id)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null) {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/User/Block/5
        public ActionResult Block(string id) {
            if (String.IsNullOrEmpty(id)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null) {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Block/5
        [HttpPost, ActionName("Block")]
        [ValidateAntiForgeryToken]
        public ActionResult BlockConfirmed(string id) {
            var user = db.Users.Find(id);
            user.IsBlocked = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/User/Block/5
        public ActionResult Unblock(string id) {
            if (String.IsNullOrEmpty(id)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null) {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Block/5
        [HttpPost, ActionName("Unblock")]
        [ValidateAntiForgeryToken]
        public ActionResult UnblockConfirmed(string id) {
            var user = db.Users.Find(id);
            user.IsBlocked = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/User/Export
        public void Export() {
            _importExportService.Export(db.Users.ToList(), "Users");
        }
    }
}