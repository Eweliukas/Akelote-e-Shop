using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Akelote_e_Shop.Models;

namespace Akelote_e_Shop.Areas.Admin.ViewModels {

    public class CategoryFormViewModel {
        public IEnumerable<Category> Categories { get; set; }
        public Category Category { get; set; }
    }
}