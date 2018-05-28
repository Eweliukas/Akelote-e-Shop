using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.Models
{
    public class Category : IIdentifiable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public int? Discount { get; set; }

        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> Children { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Property> Properties { get; set; }

        public IEnumerable<Category> Ancestors()
        {
            var category = Parent;
            while (category != null)
            {
                yield return category;
                category = category.Parent;
            }
        }

        public IEnumerable<Category> Descendants()
        {
            if (Children == null)
                return null;

            var descedants = new List<Category>(Children);

            foreach (var childDescedants in Children.Select(c => c.Descendants())) {
                descedants.AddRange(childDescedants);
            };

            return descedants;
        }

        public IEnumerable<Property> GetAllProperties()
        {
            var properties = new List<Property>(Properties);

            foreach(var category in Ancestors())
            {
                properties.AddRange(category.Properties);
            }

            return properties;
        }
    }
}