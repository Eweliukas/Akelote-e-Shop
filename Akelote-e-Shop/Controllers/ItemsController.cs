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

            return View("CustomerForm", viewModel);

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

                return View("CustomerForm", viewModel);
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

        public ViewResult Index()
        {
            var items = _context.Item.ToList();  

            return View(items);
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