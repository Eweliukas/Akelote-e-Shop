using Akelote_e_Shop.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Akelote_e_Shop.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private ApplicationDbContext _context;

        public CheckoutController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Checkout
        public ActionResult Index(Order order)
        {
            HttpContext.Session["order"] = order;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Pay(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", payment);
            }

            payment.Amount = ShoppingCart.GetCart(this).GetTotal();
            var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", 
                Convert.ToBase64String(Encoding.ASCII.GetBytes("technologines:platformos"))
            );
            var url = "https://mock-payment-processor.appspot.com/v1/payment";
            var response = await http.PostAsJsonAsync(url, payment);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                CreateOrder(ShoppingCart.GetCart(HttpContext));
                ShoppingCart.GetCart(HttpContext).EmptyCart();

                return RedirectToAction("../Orders", new { Message = "Order created successfully!" });
            }
            else
            {
                JObject jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                foreach(var message in jObject["message"].ToString().Split(new[] { "\n" }, StringSplitOptions.None))
                {
                    ModelState.AddModelError("", message);
                }
                return View("Index", payment);
            }
        }

        public ActionResult CreateOrder(ShoppingCart cart)
        {
            Order order = (Order)HttpContext.Session["order"];

            _context.Order.Add(order);

            foreach (var cartItem in cart.GetCartItems())
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ItemId = cartItem.ItemId,
                    Quantity = cartItem.Count,
                    OrderPrice = cartItem.Item.Price - (cartItem.Item.Discount ?? 0)
                };

                _context.OrderItem.Add(orderItem);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Items");
        }
    }
}