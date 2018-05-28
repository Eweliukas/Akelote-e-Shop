using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Akelote_e_Shop.Areas.Admin.ViewModels;
using Akelote_e_Shop.Models;

namespace Akelote_e_Shop.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private readonly int _limit = 10;

        public ActionResult Details(int id) 
        {
            Category category = _context.Category.Find(id);
            if (category == null) {
                HttpNotFound();
            }
            return View("Details", category);
        }

        public ActionResult Create() {
            var categories = _context.Category.ToList();

            var viewModel = new CategoryFormViewModel {
                Category = new Category(),
                Categories = categories
            };
            return View("Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category) {

            if (!ModelState.IsValid) {
                var categories = _context.Category.ToList();
                var viewModel = new CategoryFormViewModel {
                    Category = category,
                    Categories = categories
                };

                return View("Create", viewModel);
            }

            _context.Category.Add(category);

            _context.SaveChanges();

            return RedirectToAction("Details", "Category");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, int? page) {

            Category category = await _context.Category.FindAsync(id);

            if (category == null) {
                return
                    HttpNotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("List", page);

        }

        public ActionResult List(int page) {

            double temp = (page - 1) / _limit;
            int offset = (int) Math.Ceiling(temp);

            List<Category> categories = _context.Category
                .OrderBy(c => c.Title)
                .Take(10)
                .Skip(offset)
                .ToList();

            var viewModel = new CategoryListViewModel() {
                Page = page,
                Categories = categories
            };

            return View("List", viewModel);
        }

        public ActionResult Edit(int id) {

            Category category = _context.Category.Find(id);

            if (category == null) {
                HttpNotFound();
            }

            var categories = _context.Category.ToList();

            var viewModel = new CategoryFormViewModel {
                Category = category,
                Categories = categories
            };
            return View("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category) {

            if (!ModelState.IsValid) {
                var categories = _context.Category.ToList();
                var viewModel = new CategoryFormViewModel {
                    Category = category,
                    Categories = categories
                };

                return View("Edit", viewModel);
            }

            _context.Category.Add(category);

            _context.SaveChanges();

            return RedirectToAction("Details", "Category");

        }
    }
}