using Akelote_e_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.ViewModels
{
    public class SubcategoryViewModel
    {
        public IEnumerable<Category> AllCategories { get; set; }
        public Category Parent { get; set; }

        public string Title
        {
            get
            {
                return Parent.Title;
            }
        }
        public IEnumerable<SubcategoryViewModel> Children
        {
            get
            {
                var categories = AllCategories;
                if (Parent is null)
                {
                    categories = categories.Where(c => c.ParentId is null);
                }
                else
                {
                    categories = categories.Where(c => c.ParentId == Parent.Id);
                }
                return categories.Select(c => new SubcategoryViewModel
                {
                    AllCategories = AllCategories,
                    Parent = c
                });
            }
        }
    }
}