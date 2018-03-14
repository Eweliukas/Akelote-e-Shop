using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]

        public int Price { get; set; }
        public string Description { get; set; }
        public int? Discount { get; set; }

        public virtual ICollection<ItemProperty> ItemProperty { get; set; }

    }
}