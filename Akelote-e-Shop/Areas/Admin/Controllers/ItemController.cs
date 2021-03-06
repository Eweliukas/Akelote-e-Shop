﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Akelote_e_Shop.Areas.Admin.Services;
using Akelote_e_Shop.Areas.Admin.Services.DI;
using Akelote_e_Shop.Models;

namespace Akelote_e_Shop.Areas.Admin.Controllers
{
    [Authorize(Roles = "CanUseAdminAccess")]
    public class ItemController : Controller
    {
        private readonly ImportExportService _importExportService;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ItemController() {
            _importExportService = new ExcelImportExportService();
        }

        // GET: Admin/Item
        public ActionResult Index()
        {
            var item = db.Item.Include(i => i.Category);
            return View(item.ToList());
        }

        // GET: Admin/Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Admin/Item/Create
        public ActionResult Create(int? id)
        {
            if (id != null) {
                var properties = db.Category.FirstOrDefault(c => c.Id == id)?.GetAllProperties();
                ViewBag.Properties = properties;
            }

            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Title");
            return View();
        }

        // GET: Admin/Item/ItemProperties/1
        public ActionResult ItemProperties(int id) {

            Item item = db.Item.Find(id);
            if (item == null) {
                return HttpNotFound();
            }
            
            return View(item);
        }

        // POST: Admin/Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,Title,Price,Description,Discount,Deleted")] Item item, IEnumerable<HttpPostedFileBase> images)
        {
            if (ModelState.IsValid) {

                db.Item.Add(item);
                IncludeImages(item, images);
                db.SaveChanges();

//                var category = db.Category.Find(item.CategoryId);
//                ItemProperty itemProperty = new ItemProperty();
//                itemProperty.ItemPropertyId = 1;
//                itemProperty.ItemId = 14;
//                itemProperty.PropertyId = 9;
//                db.ItemProperty.Add(itemProperty);
//                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Title", item.CategoryId);
            return View(item);
        }

        private void IncludeImages(Item item, IEnumerable<HttpPostedFileBase> images)
        {
            foreach (var image in images)
            {
                var name = Guid.NewGuid() + Path.GetExtension(image.FileName);
                image.SaveAs(Path.Combine(Server.MapPath("/Content"), name));
                db.Image.Add(new Image { Item = item, Caption = item.Title, HyperLink = name });
            }
        }

        // GET: Admin/Item/Edit/5
        public ActionResult Edit(int id)
        {
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Title", item.CategoryId);
            return View(item);
        }

        // POST: Admin/Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,Title,Price,Description,Discount,Deleted")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Title", item.CategoryId);
            return View(item);
        }

        // GET: Admin/Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Admin/Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Item.Find(id);
            db.Item.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Admin/Item/Export
        public void Export() {
            _importExportService.Export(db.Item.ToList(), "Items");
        }
    }
}
