using Akelote_e_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.ViewModels
{
    public class ItemFormViewModel
    {
        public bool Editing
        {
            get
            {
                return Item.Id != 0;
            }
        }
        public IEnumerable<Category> Categories { get; set; }
        public Item Item { get; set; }
    }
}