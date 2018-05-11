using Akelote_e_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Akelote_e_Shop.ViewModels;

namespace Akelote_e_Shop.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext _context;

        public ItemsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var categories = _context.Category.ToList();
            var viewModel = new ItemFormViewModel
            {
                Item = new Item(),
                Categories = categories
            };

            return View("itemForm", viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Item item)
        {
            if (!ModelState.IsValid)
            {
                var categories = _context.Category.ToList();
                var viewModel = new ItemFormViewModel
                {
                    Item = item,
                    Categories = categories
                };

                return View("itemForm", viewModel);
            }

            if (item.Id == 0)
                _context.Item.Add(item);
            else
            {
                var itemInDb = _context.Item.Single(c => c.Id == item.Id);
                itemInDb.Title = item.Title;
                itemInDb.Price = item.Price;
                itemInDb.CategoryId = item.CategoryId;
                itemInDb.Description = item.Description;
                itemInDb.Discount = item.Discount;

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "items");
        }

        public ActionResult Index(int? categoryId = null)
        {
            IEnumerable<Item> items = _context.Item;
            var categories = _context.Category.ToList();
            IEnumerable<Category> categoryAndAncestors = new Category[] { };
            if (categoryId != null)
            {
                var pivot = categories.SingleOrDefault(c => c.Id == categoryId);

                if (pivot == null)
                    return HttpNotFound();

                var categoryAndDescendants = pivot.Descendants(categories).Union(new[] { pivot });

                items = items.Where(i => categoryAndDescendants.Select(c => c.Id).Contains(i.CategoryId));

                categoryAndAncestors = pivot.Ancestors(categories).Union(new[] { pivot });
            }
            items = items.ToList();
            return View(new ItemListViewModel
            {
                Items = items,
                CategoryAndAncestors = categoryAndAncestors
            });
        }



        public ActionResult Edit(int id)
        {
            var item = _context.Item.SingleOrDefault(c => c.Id == id);

            if (item == null)
                return HttpNotFound();

            var viewModel = new ItemFormViewModel
            {
                Item = item,
                Categories = _context.Category.ToList()
            };

            return View("itemForm", viewModel);
        }

        public ActionResult Details(int id)
        {
            var item = _context.Item.SingleOrDefault(c => c.Id == id);

            if (item == null)
                return HttpNotFound();

            return View("details", item);
        }
    }
}