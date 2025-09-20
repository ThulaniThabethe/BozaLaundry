using System.Web.Mvc;
using WebApplication1.Services;
using WebApplication1.Models;
using System.Net;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomersController()
        {
            // In a real application, use a dependency injection container
            var context = new BozaLaundryContext();
            var unitOfWork = new Repositories.UnitOfWork(context);
            _customerService = new CustomerService(unitOfWork);
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = _customerService.GetAllCustomers();
            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerService.GetCustomerById(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Address,PhoneNumber,Email,RegistrationDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.AddCustomer(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerService.GetCustomerById(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,FirstName,LastName,Address,PhoneNumber,Email,RegistrationDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.UpdateCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerService.GetCustomerById(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _customerService.DeleteCustomer(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose the UnitOfWork if it implements IDisposable
                // (_customerService.UnitOfWork as IDisposable)?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}