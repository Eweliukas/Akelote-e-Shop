using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Akelote_e_Shop.Models;
using Microsoft.VisualBasic.ApplicationServices;

namespace Akelote_e_Shop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/User
        public ActionResult Index() {
            var user = db.Users.Include(c => c.Orders);
            return View(user.ToList());
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null) {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
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
        public ActionResult Block(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null) {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Block/5
        [HttpPost, ActionName("Put")]
        [ValidateAntiForgeryToken]
        public ActionResult BlockConfirmed(int id) {
            var user = db.Users.Find(id);
            user.IsBlocked = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/User/Unblock/5
        public ActionResult Unblock(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null) {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Unblock/5
        [HttpPost, ActionName("Put")]
        [ValidateAntiForgeryToken]
        public ActionResult UnblockConfirmed(int id) {
            var user = db.Users.Find(id);
            user.IsBlocked = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}