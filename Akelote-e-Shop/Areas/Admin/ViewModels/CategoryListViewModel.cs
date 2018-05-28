using System.Collections.Generic;
using Akelote_e_Shop.Models;

namespace Akelote_e_Shop.Areas.Admin.ViewModels {
    public class CategoryListViewModel {
        public List<Category> Categories { get; set; }
        public int Page { get; set; }
    }
}