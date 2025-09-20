using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication1.Models;
using System;

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
            var orders = db.Orders.Include(o => o.Customer);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include(o => o.Customer).Include(o => o.OrderItems.Select(oi => oi.Service)).FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,OrderDate,PickupDate,DeliveryDate,Status,TotalAmount")] Order order, int[] serviceIds, decimal[] quantities)
        {
            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                order.Status = "Pending";
                order.TotalAmount = 0;

                db.Orders.Add(order);
                db.SaveChanges();

                if (serviceIds != null && quantities != null)
                {
                    for (int i = 0; i < serviceIds.Length; i++)
                    {
                        var service = db.Services.Find(serviceIds[i]);
                        if (service != null)
                        {
                            var orderItem = new OrderItem
                            {
                                OrderId = order.Id,
                                ServiceId = service.Id,
                                Quantity = quantities[i],
                                PricePerUnit = service.Price
                            };
                            db.OrderItems.Add(orderItem);
                            order.TotalAmount += (orderItem.Quantity * orderItem.PricePerUnit);
                        }
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", order.CustomerId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name");
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", order.CustomerId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name");
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,OrderDate,PickupDate,DeliveryDate,Status,TotalAmount")] Order order, int[] serviceIds, decimal[] quantities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.OrderItems.RemoveRange(db.OrderItems.Where(oi => oi.OrderId == order.Id));
                order.TotalAmount = 0;

                if (serviceIds != null && quantities != null)
                {
                    for (int i = 0; i < serviceIds.Length; i++)
                    {
                        var service = db.Services.Find(serviceIds[i]);
                        if (service != null)
                        {
                            var orderItem = new OrderItem
                            {
                                OrderId = order.Id,
                                ServiceId = service.Id,
                                Quantity = quantities[i],
                                PricePerUnit = service.Price
                            };
                            db.OrderItems.Add(orderItem);
                            order.TotalAmount += (orderItem.Quantity * orderItem.PricePerUnit);
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", order.CustomerId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name");
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include(o => o.Customer).FirstOrDefault(o => o.Id == id);
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
            db.OrderItems.RemoveRange(db.OrderItems.Where(oi => oi.OrderId == id));
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