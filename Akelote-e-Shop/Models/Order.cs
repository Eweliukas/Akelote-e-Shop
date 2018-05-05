using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
        public string Destination { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DisplayName("Order discount")]
        public int? OrderDiscount { get; set; }
        public int? Rating { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public int GetTotal()
        {
            return OrderItems.Select(item => item.OrderPrice * item.Quantity).Sum();
        }
    }
}