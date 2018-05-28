using Akelote_e_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Akelote_e_Shop.Controllers
{
    [Authorize(Roles=Roles.Admin)]
    public class AdminController<Entity> : Controller where Entity : class, IIdentifiable
    {
        public const int PageSize = 20;

        private ApplicationDbContext _context;

        public string Name
        {
            get
            {
                return HttpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            }
        }

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(int page = 1)
        {
            var entities = _context.Set<Entity>().OrderByDescending(e => e.Id);
            var entitiesInPage = entities.Skip((page - 1) * PageSize).Take(PageSize).ToArray();
            return View("~/Views/Admin/Index.cshtml", entitiesInPage.Select(item => Degenericize(item)));
        }

        public ActionResult Edit(int id)
        {
            var entity = _context.Set<Entity>().Single(e => e.Id == id);
            return View("~/Views/Admin/Edit.cshtml", Degenericize(entity));
        }

        [HttpPost]
        public ActionResult Save(Entity data)
        {
            var entity = _context.Set<Entity>().SingleOrDefault(e => e.Id == data.Id);
            UpdateModel(entity);
            _context.SaveChanges();
            return RedirectToAction("Index", Name);
        }

        public ActionResult Delete(int id)
        {
            var entity = _context.Set<Entity>().Single(e => e.Id == id);
            return View("~/Views/Admin/Delete.cshtml", entity);
        }

        [HttpPost]
        public ActionResult PerformDeletion(int id)
        {
            var entities = _context.Set<Entity>();
            entities.Remove(entities.Where(e => e.Id == id).Single());
            _context.SaveChanges();
            return RedirectToAction("Index", Name);
        }

        public Dictionary<string, Union> Degenericize(Entity entity)
        {
            var result = new Dictionary<string, Union>();
            var type = entity.GetType();
            foreach (var property in type.GetProperties())
            {
                result[property.Name] = new Union(property.GetValue(entity));
            }
            return result;
        }
    }
}