using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Orders_Placing.Models;

namespace Orders_Placing.Controllers
{
    public class Products_InventoryController : Controller
    {
        private Entities db = new Entities();

        // GET: Products_Inventory
        public ActionResult Index()
        {
            var products_Inventory = db.Products_Inventory.Include(p => p.Accountant);
            return View(products_Inventory.ToList());
        }

        // GET: Products_Inventory/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_Inventory products_Inventory = db.Products_Inventory.Find(id);
            if (products_Inventory == null)
            {
                return HttpNotFound();
            }
            return View(db.Products_Inventory);
        }

        // GET: Products_Inventory/Create
        public ActionResult Create()
        {
            ViewBag.accountantName = new SelectList(db.Accountants, "userName", "password");
            return View();
        }

        // POST: Products_Inventory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productId,productName,accountantName,importDate,exportDate,price,quantity,total")] Products_Inventory products_Inventory)
        {
            if (ModelState.IsValid)
            {
                db.Products_Inventory.Add(products_Inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.accountantName = new SelectList(db.Accountants, "userName", "password", products_Inventory.accountantName);
            return View(products_Inventory);
        }

        // GET: Products_Inventory/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_Inventory products_Inventory = db.Products_Inventory.Find(id);
            if (products_Inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountantName = new SelectList(db.Accountants, "userName", "password", products_Inventory.accountantName);
            return View(products_Inventory);
        }

        // POST: Products_Inventory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productId,productName,accountantName,importDate,exportDate,price,quantity,total")] Products_Inventory products_Inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products_Inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.accountantName = new SelectList(db.Accountants, "userName", "password", products_Inventory.accountantName);
            return View(products_Inventory);
        }

        // GET: Products_Inventory/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_Inventory products_Inventory = db.Products_Inventory.Find(id);
            if (products_Inventory == null)
            {
                return HttpNotFound();
            }
            return View(products_Inventory);
        }

        // POST: Products_Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Products_Inventory products_Inventory = db.Products_Inventory.Find(id);
            db.Products_Inventory.Remove(products_Inventory);
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

        public ActionResult Orders_Item(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_Inventory products_Inventory = db.Products_Inventory.Find(id);
            if (products_Inventory == null)
            {
                return HttpNotFound();
            }
            return View(db.Products_Inventory);
        }
    }
}
