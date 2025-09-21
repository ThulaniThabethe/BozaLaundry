extern alias EF;
using EF::System.Data.Entity;
using EF::System.Data.Entity.Infrastructure;
using EF::System.Data.Entity.Migrations;
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
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var customerProfile = db.CustomerProfiles.FirstOrDefault(c => c.CustomerId == userId);

            if (customerProfile == null)
            {
                return RedirectToAction("Create");
            }
            return View(customerProfile);
        }

        // GET: Customer/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProfile customerProfile = db.CustomerProfiles.FirstOrDefault(c => c.CustomerId == id);
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
        public ActionResult Create([Bind(Include = "FirstName,LastName,Address,PhoneNumber,PreferredContactMethod")] CustomerProfile customerProfile)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                customerProfile.CustomerId = userId;
                db.CustomerProfiles.Add(customerProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerProfile);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProfile customerProfile = db.CustomerProfiles.Find(id);
            if (customerProfile == null)
            {
                return HttpNotFound();
            }
            return View(customerProfile);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,FirstName,LastName,Address,PhoneNumber,PreferredContactMethod")] CustomerProfile customerProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerProfile).State = EF::System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerProfile);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProfile customerProfile = db.CustomerProfiles.Find(id);
            if (customerProfile == null)
            {
                return HttpNotFound();
            }
            return View(customerProfile);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CustomerProfile customerProfile = db.CustomerProfiles.Find(id);
            db.CustomerProfiles.Remove(customerProfile);
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