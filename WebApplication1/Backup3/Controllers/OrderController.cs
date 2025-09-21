extern alias EF;
using System;
using EF::System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;
using EF::System.Data.Entity.Infrastructure;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Order
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var orders = db.Orders.Include(o => o.CustomerProfile).Include(o => o.ServiceType).Include(o => o.OrderStatus).Where(o => o.CustomerProfile.CustomerId == userId);
            return View(orders.ToList());
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            Order order = db.Orders.Include(o => o.CustomerProfile).Include(o => o.ServiceType).Include(o => o.OrderStatus).FirstOrDefault(o => o.OrderId == id && o.CustomerProfile.CustomerId == userId);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes, "ServiceTypeId", "Name");
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceTypeId,Weight,SpecialInstructions")] Order order)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var customerProfile = db.CustomerProfiles.FirstOrDefault(c => c.CustomerId == userId);
                if (customerProfile == null)
                {
                    // Handle case where customer profile doesn't exist, perhaps create one or return an error
                    // For now, let's assume it exists or create a new one if not.
                    customerProfile = new CustomerProfile { CustomerId = userId };
                    db.CustomerProfiles.Add(customerProfile);
                    db.SaveChanges();
                }
                order.CustomerProfile = customerProfile;
                order.OrderDate = System.DateTime.Now;
                order.StatusId = db.OrderStatuses.FirstOrDefault(s => s.StatusName == "Pending").OrderStatusId; // Set initial status to Pending

                var serviceType = db.ServiceTypes.Find(order.ServiceTypeId);
                if (serviceType != null)
                {
                    if (serviceType.BundlePrice.HasValue)
                    {
                        order.TotalPrice = serviceType.BundlePrice.Value;
                    }
                    else if (order.Weight.HasValue && serviceType.MinWeight.HasValue && serviceType.MaxWeight.HasValue && order.Weight >= (double)serviceType.MinWeight.Value && order.Weight <= (double)serviceType.MaxWeight.Value)
                    {
                        order.TotalPrice = (decimal)(order.Weight.Value * (double)serviceType.PricePerUnit);
                    }
                    else
                    {
                        order.TotalPrice = (decimal)(order.Weight.Value * (double)serviceType.PricePerUnit);
                    }
                }

                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes, "ServiceTypeId", "Name", order.ServiceTypeId);
            return View(order);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes, "ServiceTypeId", "Name", order.ServiceTypeId);
            ViewBag.StatusId = new SelectList(db.OrderStatuses, "OrderStatusId", "StatusName", order.StatusId);
            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CustomerId,OrderDate,StatusId,ServiceTypeId,Weight,TotalPrice,PickupDate,DeliveryDate,SpecialInstructions")] Order order)
        {
            if (ModelState.IsValid)
            {
                var existingOrder = db.Orders.Find(order.OrderId);
                if (existingOrder == null)
                {
                    return HttpNotFound();
                }

                existingOrder.StatusId = order.StatusId;
                existingOrder.ServiceTypeId = order.ServiceTypeId;
                existingOrder.Weight = order.Weight;
                existingOrder.PickupDate = order.PickupDate;
                existingOrder.DeliveryDate = order.DeliveryDate;
                existingOrder.SpecialInstructions = order.SpecialInstructions;

                // Check if the status is changing to 'Completed'
                var completedStatus = db.OrderStatuses.FirstOrDefault(s => s.StatusName == "Completed");
                if (order.StatusId == completedStatus.OrderStatusId && existingOrder.StatusId != completedStatus.OrderStatusId)
                {
                    existingOrder.InvoiceNumber = Guid.NewGuid().ToString(); // Generate a unique invoice number
                    existingOrder.ReceiptGeneratedDate = DateTime.Now;
                    existingOrder.IsInvoiceGenerated = true;
                }
                existingOrder.StatusId = order.StatusId;

                var serviceType = db.ServiceTypes.Find(existingOrder.ServiceTypeId);
                if (serviceType != null)
                {
                    if (serviceType.BundlePrice.HasValue)
                    {
                        existingOrder.TotalPrice = serviceType.BundlePrice.Value;
                    }
                    else if (existingOrder.Weight.HasValue && serviceType.MinWeight.HasValue && serviceType.MaxWeight.HasValue && existingOrder.Weight >= (double)serviceType.MinWeight.Value && existingOrder.Weight <= (double)serviceType.MaxWeight.Value)
                    {
                        existingOrder.TotalPrice = (decimal)(existingOrder.Weight.Value * (double)serviceType.PricePerUnit);
                    }
                    else
                    {
                        existingOrder.TotalPrice = (decimal)(existingOrder.Weight.Value * (double)serviceType.PricePerUnit);
                    }
                }

                db.Entry(existingOrder).State = EF::System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes, "ServiceTypeId", "Name", order.ServiceTypeId);
            ViewBag.StatusId = new SelectList(db.OrderStatuses, "OrderStatusId", "StatusName", order.StatusId);
            return View(order);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            Order order = db.Orders.Include(o => o.ServiceType).Include(o => o.OrderStatus).Include(o => o.CustomerProfile).FirstOrDefault(o => o.OrderId == id && o.CustomerProfile.CustomerId == userId);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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