using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin")] // Only administrators can manage service types
    public class ServiceTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServiceType
        public async Task<ActionResult> Index()
        {
            return View(await db.ServiceTypes.ToListAsync());
        }

        // GET: ServiceType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = await db.ServiceTypes.FindAsync(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            return View(serviceType);
        }

        // GET: ServiceType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ServiceTypeId,ServiceTypeName,PricePerUnit,Description")] ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                db.ServiceTypes.Add(serviceType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(serviceType);
        }

        // GET: ServiceType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = await db.ServiceTypes.FindAsync(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            return View(serviceType);
        }

        // POST: ServiceType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ServiceTypeId,ServiceTypeName,PricePerUnit,Description")] ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(serviceType);
        }

        // GET: ServiceType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = await db.ServiceTypes.FindAsync(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            return View(serviceType);
        }

        // POST: ServiceType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ServiceType serviceType = await db.ServiceTypes.FindAsync(id);
            db.ServiceTypes.Remove(serviceType);
            await db.SaveChangesAsync();
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