using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        [Required]
        public int OrderPrice { get; set; }
    }
}