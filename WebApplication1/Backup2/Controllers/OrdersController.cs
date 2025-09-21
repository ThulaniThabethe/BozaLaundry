extern alias EF;
using EF::System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderList()
        {
            var orders = db.Orders.Include(o => o.CustomerProfile).Include(o => o.OrderStatus);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include(o => o.CustomerProfile).Include(o => o.OrderStatus).Include(o => o.ServiceType).Include(o => o.OrderItems).FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.CustomerProfiles, "Id", "Name");
            ViewBag.ServiceId = new SelectList(db.ServiceTypes, "ServiceTypeId", "Name");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,CustomerId,OrderDate,PickupDate,DeliveryDate,StatusId,TotalPrice")] Order order, int[] serviceIds, decimal[] quantities)
        {
            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                order.StatusId = 1; // Assuming 1 is Pending status
                order.TotalPrice = 0;

                db.Orders.Add(order);
                db.SaveChanges();

                if (serviceIds != null && quantities != null)
                {
                    for (int i = 0; i < serviceIds.Length; i++)
                    {
                        var service = db.ServiceTypes.Find(serviceIds[i]);
                        if (service != null)
                        {
                            var orderItem = new OrderItem
                            {
                                OrderId = order.OrderId,
                                ServiceId = service.ServiceTypeId,
                                Quantity = quantities[i],
                                PricePerUnit = service.PricePerUnit
                            };
                            db.OrderItems.Add(orderItem);
                            order.TotalPrice += (orderItem.Quantity * orderItem.PricePerUnit);
                        }
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.CustomerProfiles, "Id", "Name", order.CustomerId);
            ViewBag.ServiceId = new SelectList(db.ServiceTypes, "ServiceTypeId", "Name");
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.CustomerProfiles, "Id", "Name", order.CustomerId);
            ViewBag.ServiceId = new SelectList(db.ServiceTypes, "ServiceTypeId", "Name");
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CustomerId,OrderDate,PickupDate,DeliveryDate,StatusId,TotalPrice")] Order order, int[] serviceIds, decimal[] quantities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EF::System.Data.Entity.EntityState.Modified;
                db.OrderItems.RemoveRange(db.OrderItems.Where(oi => oi.OrderId == order.OrderId));
                order.TotalPrice = 0;

                if (serviceIds != null && quantities != null)
                {
                    for (int i = 0; i < serviceIds.Length; i++)
                    {
                        var service = db.ServiceTypes.Find(serviceIds[i]);
                        if (service != null)
                        {
                            var orderItem = new OrderItem
                            {
                                OrderId = order.OrderId,
                                ServiceId = service.ServiceTypeId,
                                Quantity = quantities[i],
                                PricePerUnit = service.PricePerUnit
                            };
                            db.OrderItems.Add(orderItem);
                            order.TotalPrice += (orderItem.Quantity * orderItem.PricePerUnit);
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.CustomerProfiles, "Id", "Name", order.CustomerId);
            ViewBag.ServiceId = new SelectList(db.ServiceTypes, "ServiceTypeId", "Name");
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include(o => o.CustomerProfile).FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                db.OrderItems.RemoveRange(db.OrderItems.Where(oi => oi.OrderId == id));
                db.Orders.Remove(order);
                db.SaveChanges();
            }
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