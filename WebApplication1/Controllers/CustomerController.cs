using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var customerProfile = await db.CustomerProfiles.Include(c => c.ApplicationUser).FirstOrDefaultAsync(c => c.CustomerId == userId);

            if (customerProfile == null)
            {
                return RedirectToAction("Create");
            }
            return View(customerProfile);
        }

        // GET: Customer/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProfile customerProfile = await db.CustomerProfiles.Include(c => c.ApplicationUser).FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customerProfile == null)
            {
                return HttpNotFound();
            }
            return View(customerProfile);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var customerProfile = db.CustomerProfiles.Find(userId);
            if (customerProfile != null)
            {
                return RedirectToAction("Edit");
            }
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FirstName,LastName,Address,PhoneNumber,PreferredContactMethod")] CustomerProfile customerProfile)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                customerProfile.CustomerId = userId;
                db.CustomerProfiles.Add(customerProfile);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customerProfile);
        }

        // GET: Customer/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProfile customerProfile = await db.CustomerProfiles.FindAsync(id);
            if (customerProfile == null)
            {
                return HttpNotFound();
            }
            return View(customerProfile);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CustomerId,FirstName,LastName,Address,PhoneNumber,PreferredContactMethod")] CustomerProfile customerProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerProfile).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customerProfile);
        }

        // GET: Customer/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProfile customerProfile = await db.CustomerProfiles.FindAsync(id);
            if (customerProfile == null)
            {
                return HttpNotFound();
            }
            return View(customerProfile);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            CustomerProfile customerProfile = await db.CustomerProfiles.FindAsync(id);
            db.CustomerProfiles.Remove(customerProfile);
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