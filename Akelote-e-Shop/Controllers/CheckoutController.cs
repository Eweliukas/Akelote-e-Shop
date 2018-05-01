using Akelote_e_Shop.Models;
using Akelote_e_Shop.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Akelote_e_Shop.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
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
                ShoppingCart.GetCart(HttpContext).EmptyCart();
                // TODO: populate Order model
                return View("Success");
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
    }
}