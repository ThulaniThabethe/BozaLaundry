using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class InventoryController : Controller
    {
        private BozaLaundryContext db = new BozaLaundryContext();

        // GET: Inventory
        public ActionResult Index()
        {
            return View(db.InventoryItems.ToList());
        }

        // GET: Inventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryItem inventoryItem = db.InventoryItems.Find(id);
            if (inventoryItem == null)
            {
                return HttpNotFound();
            }
            return View(inventoryItem);
        }

        // GET: Inventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Quantity,Unit,LastUpdated")] InventoryItem inventoryItem)
        {
            if (ModelState.IsValid)
            {
                db.InventoryItems.Add(inventoryItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inventoryItem);
        }

        // GET: Inventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryItem inventoryItem = db.InventoryItems.Find(id);
            if (inventoryItem == null)
            {
                return HttpNotFound();
            }
            return View(inventoryItem);
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Quantity,Unit,LastUpdated")] InventoryItem inventoryItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventoryItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventoryItem);
        }

        // GET: Inventory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryItem inventoryItem = db.InventoryItems.Find(id);
            if (inventoryItem == null)
            {
                return HttpNotFound();
            }
            return View(inventoryItem);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InventoryItem inventoryItem = db.InventoryItems.Find(id);
            db.InventoryItems.Remove(inventoryItem);
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
    }
}