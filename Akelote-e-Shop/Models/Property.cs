using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.Models
{
    public class Property
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int CategoryId { get; set; }

        public virtual ICollection<ItemProperty> ItemProperty { get; set; }

    }
}