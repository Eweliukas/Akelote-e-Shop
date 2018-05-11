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

        public IEnumerable<Category> Ancestors(IEnumerable<Category> all)
        {
            var currentId = ParentId;
            while (currentId != null)
            {
                var current = all.SingleOrDefault(c => c.Id == currentId);
                currentId = null;
                if (current != null)
                {
                    yield return current;
                    currentId = current.ParentId;
                }
            }
        }

        public IEnumerable<Category> Descendants(IEnumerable<Category> all)
        {
            foreach (var category in all)
            {
                if (category.ParentId == Id)
                {
                    yield return category;
                    foreach (var descendant in category.Descendants(all))
                    {
                        yield return descendant;
                    }
                }
            }
        }
    }
}