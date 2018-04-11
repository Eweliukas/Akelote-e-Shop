using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public int? Discount { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

    }
}