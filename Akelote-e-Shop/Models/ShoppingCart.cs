using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Akelote_e_Shop.Models
{
    public partial class ShoppingCart
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        public const string CartSessionKey = "CartId";

        string ShoppingCartId { get; set; }

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);

            return cart;
        }
        
        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Item item)
        {
            // Get the matching cart and album instances
            var cartItem = storeDB.Cart.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ItemId == item.Id);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    ItemId = item.Id,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                storeDB.Cart.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count++;
            }

            storeDB.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = storeDB.Cart.Single(
                cart => cart.CartId == ShoppingCartId && cart.RecordId == id);


            if (cartItem == null)
                return 0;

            cartItem.Count--;
            var itemCount = cartItem.Count;

            if (itemCount == 0)
                storeDB.Cart.Remove(cartItem);

            storeDB.SaveChanges();

            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = storeDB.Cart.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
                storeDB.Cart.Remove(cartItem);

            storeDB.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return storeDB.Cart.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in storeDB.Cart
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in storeDB.Cart
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Item.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            int orderTotal = 0;

            var cartItems = GetCartItems();
            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    Id = item.ItemId,
                    OrderId = order.Id,
                    OrderPrice = item.Item.Price,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Item.Price);

                storeDB.OrderItem.Add(orderItem);
            }

            order.Total = orderTotal;

            storeDB.SaveChanges();
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.Id;
        }
        
        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.Cart.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
                item.CartId = userName;
 
            storeDB.SaveChanges();
        }
    }
}