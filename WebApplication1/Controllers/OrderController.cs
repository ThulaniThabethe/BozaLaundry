using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Order
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var orders = db.Orders.Include(o => o.CustomerProfile).Include(o => o.ServiceType).Include(o => o.OrderStatus).Where(o => o.CustomerProfile.CustomerId == userId);
            return View(await orders.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.Include(o => o.CustomerProfile).Include(o => o.ServiceType).Include(o => o.OrderStatus).FirstOrDefaultAsync(o => o.OrderId == id && o.CustomerProfile.CustomerId == userId);
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
        public async Task<ActionResult> Create([Bind(Include = "ServiceTypeId,Weight,SpecialInstructions")] Order order)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var customerProfile = await db.CustomerProfiles.FirstOrDefaultAsync(c => c.CustomerId == userId);
                if (customerProfile == null)
                {
                    // Handle case where customer profile doesn't exist, perhaps create one or return an error
                    // For now, let's assume it exists or create a new one if not.
                    customerProfile = new CustomerProfile { CustomerId = userId };
                    db.CustomerProfiles.Add(customerProfile);
                    await db.SaveChangesAsync();
                }
                order.CustomerProfile = customerProfile;
                order.OrderDate = System.DateTime.Now;
                order.StatusId = db.OrderStatuses.FirstOrDefault(s => s.StatusName == "Pending").OrderStatusId; // Set initial status to Pending

                var serviceType = await db.ServiceTypes.FindAsync(order.ServiceTypeId);
                if (serviceType != null)
                {
                    if (serviceType.BundlePrice.HasValue)
                    {
                        order.TotalPrice = serviceType.BundlePrice.Value;
                    }
                    else if (order.Weight.HasValue && serviceType.MinWeight.HasValue && serviceType.MaxWeight.HasValue && order.Weight >= serviceType.MinWeight.Value && order.Weight <= serviceType.MaxWeight.Value)
                    {
                        order.TotalPrice = order.Weight.Value * serviceType.PricePerUnit;
                    }
                    else
                    {
                        order.TotalPrice = order.Weight * serviceType.PricePerUnit;
                    }
                }

                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes, "ServiceTypeId", "Name", order.ServiceTypeId);
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "OrderId,CustomerId,OrderDate,StatusId,ServiceTypeId,Weight,TotalPrice,PickupDate,DeliveryDate,SpecialInstructions")] Order order)
        {
            if (ModelState.IsValid)
            {
                var existingOrder = await db.Orders.FindAsync(order.OrderId);
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

                var serviceType = await db.ServiceTypes.FindAsync(existingOrder.ServiceTypeId);
                if (serviceType != null)
                {
                    if (serviceType.BundlePrice.HasValue)
                    {
                        existingOrder.TotalPrice = serviceType.BundlePrice.Value;
                    }
                    else if (existingOrder.Weight.HasValue && serviceType.MinWeight.HasValue && serviceType.MaxWeight.HasValue && existingOrder.Weight >= serviceType.MinWeight.Value && existingOrder.Weight <= serviceType.MaxWeight.Value)
                    {
                        existingOrder.TotalPrice = existingOrder.Weight.Value * serviceType.PricePerUnit;
                    }
                    else
                    {
                        existingOrder.TotalPrice = existingOrder.Weight * serviceType.PricePerUnit;
                    }
                }

                db.Entry(existingOrder).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes, "ServiceTypeId", "Name", order.ServiceTypeId);
            ViewBag.StatusId = new SelectList(db.OrderStatuses, "OrderStatusId", "StatusName", order.StatusId);
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.Include(o => o.ServiceType).Include(o => o.OrderStatus).Include(o => o.CustomerProfile).FirstOrDefaultAsync(o => o.OrderId == id && o.CustomerProfile.CustomerId == userId);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
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