using Akelote_e_Shop.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Akelote_e_Shop.Controllers
{
    public class OrderInfoController : Controller
    {
        private ApplicationDbContext _context;

        public OrderInfoController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: OrderInfo
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = _context.Users.FirstOrDefault(
                user => user.Id == currentUserId);

            var order = new Order
            {
                User = currentUser,
                UserId = currentUserId,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Phone = currentUser.PhoneNumber,
                Email = currentUser.Email,
                Destination = currentUser.Address
            };

            return View(order);
        }

        // POST: OrderInfo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Order model)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = _context.Users.FirstOrDefault(
                user => user.Id == currentUserId);

            model.Date = DateTime.Now;
            model.Status = OrderStatus.Pending;
            model.User = currentUser;
            model.UserId = currentUserId;

            return RedirectToAction("Index", "Checkout", model);
        }
    }
}
