﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index(string search)
        {
            var products = db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search));
            }

            ViewBag.Search = search; // Pass the search term back to the view
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();

                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true, message = "Product created successfully!" });
                }

                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => new { e.ErrorMessage });
                return Json(new { success = false, errors = errors });
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true, message = "Product updated successfully!" });
                }

                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => new { e.ErrorMessage });
                return Json(new { success = false, errors = errors });
            }

            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Products/AjaxDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxDelete(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return Json(new { success = false, message = "Product not found." });
            }

            db.Products.Remove(product);
            db.SaveChanges();
            return Json(new { success = true, message = "Product deleted successfully!" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
