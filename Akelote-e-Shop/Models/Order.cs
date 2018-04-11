using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
        public string Destination { get; set; }
        public string Description { get; set; }
        [Required]
        public int Total { get; set; }
        public int? OrderDiscount { get; set; }
        public int? Rating { get; set; }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }


    }
}