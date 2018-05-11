using Akelote_e_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.ViewModels
{
    public class ItemListViewModel
    {
        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<Category> CategoryAndAncestors { get; set; }

    }
}