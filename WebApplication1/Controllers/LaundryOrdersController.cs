using System.Web.Mvc;
using WebApplication1.Services;
using WebApplication1.Models;
using System.Net;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;

namespace WebApplication1.Controllers
{
    public class LaundryOrdersController : Controller
    {
        private readonly LaundryOrderService _laundryOrderService;
        private readonly CustomerService _customerService;

        public LaundryOrdersController()
        {
            // In a real application, use a dependency injection container
            var context = new BozaLaundryContext();
            var unitOfWork = new Repositories.UnitOfWork(context);
            _laundryOrderService = new LaundryOrderService(unitOfWork);
            _customerService = new CustomerService(unitOfWork);
        }

        // GET: LaundryOrders
        public ActionResult Index()
        {
            var laundryOrders = _laundryOrderService.GetAllLaundryOrders();
            return View(laundryOrders.ToList());
        }

        // GET: LaundryOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaundryOrder laundryOrder = _laundryOrderService.GetLaundryOrderById(id.Value);
            if (laundryOrder == null)
            {
                return HttpNotFound();
            }
            return View(laundryOrder);
        }

        // GET: LaundryOrders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(_customerService.GetAllCustomers(), "CustomerId", "FirstName");
            return View();
        }

        // POST: LaundryOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,ServiceType,Weight,ItemCount,OrderDate,PickupDate,DeliveryDate,Status,TotalPrice,Notes")] LaundryOrder laundryOrder)
        {
            if (ModelState.IsValid)
            {
                _laundryOrderService.AddLaundryOrder(laundryOrder);
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(_customerService.GetAllCustomers(), "CustomerId", "FirstName", laundryOrder.CustomerId);
            return View(laundryOrder);
        }

        // GET: LaundryOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaundryOrder laundryOrder = _laundryOrderService.GetLaundryOrderById(id.Value);
            if (laundryOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(_customerService.GetAllCustomers(), "CustomerId", "FirstName", laundryOrder.CustomerId);
            return View(laundryOrder);
        }

        // POST: LaundryOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CustomerId,ServiceType,Weight,ItemCount,OrderDate,PickupDate,DeliveryDate,Status,TotalPrice,Notes")] LaundryOrder laundryOrder)
        {
            if (ModelState.IsValid)
            {
                _laundryOrderService.UpdateLaundryOrder(laundryOrder);
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(_customerService.GetAllCustomers(), "CustomerId", "FirstName", laundryOrder.CustomerId);
            return View(laundryOrder);
        }

        // GET: LaundryOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaundryOrder laundryOrder = _laundryOrderService.GetLaundryOrderById(id.Value);
            if (laundryOrder == null)
            {
                return HttpNotFound();
            }
            return View(laundryOrder);
        }

        // POST: LaundryOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _laundryOrderService.DeleteLaundryOrder(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose the UnitOfWork if it implements IDisposable
                // (_laundryOrderService.UnitOfWork as IDisposable)?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}