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
    }
}