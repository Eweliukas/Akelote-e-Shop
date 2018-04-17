using Akelote_e_Shop.Models;
using Akelote_e_Shop.ViewModels;
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
                return View("Error", new PaymentResponseViewModel
                {
                    Message = await response.Content.ReadAsStringAsync()
                });
            }
        }
    }
}