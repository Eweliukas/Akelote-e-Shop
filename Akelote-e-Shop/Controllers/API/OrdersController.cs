using Akelote_e_Shop.Models;
using System.Linq;
using System.Web.Mvc;

namespace Akelote_e_Shop.Controllers.API
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context;

        public OrdersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Orders
        public ActionResult Index(string message)
        {
            ViewBag.StatusMessage = message;

            var model = _context.Order.ToList();

            return View(model);
        }

        // GET: Orders/Details/1
        public ActionResult Details(int id)
        {
            var model = _context.Order.SingleOrDefault(c => c.Id == id);

            return View(model);
        }

        // POST: Orders/Rate/5
        [HttpPost]
        public ActionResult Rate(int id, int rating)
        {
            _context.Order.SingleOrDefault(c => c.Id == id).Rating = rating;
            _context.SaveChanges();

            return View();
        }
    }
}
