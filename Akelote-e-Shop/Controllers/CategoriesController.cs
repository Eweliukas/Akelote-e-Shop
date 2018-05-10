using Akelote_e_Shop.Models;
using Akelote_e_Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Akelote_e_Shop.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context;

        public CategoriesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult ListForNavigation()
        {
            return PartialView(new SubcategoryViewModel
            {
                AllCategories = _context.Category.ToList(),
                Parent = null
            });
        }
    }
}