using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class ServicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static List<Service> _mockServices;

        public ServicesController()
        {
            if (_mockServices == null)
            {
                _mockServices = new List<Service>
                {
                    new Service 
                    { 
                        Id = 1, 
                        Name = "Wash & Fold", 
                        Description = "Professional wash, dry, and fold service for everyday laundry.", 
                        PricePerKg = 25.00m, 
                        MinimumWeightKg = 2, 
                        IsAvailable = true 
                    },
                    new Service 
                    { 
                        Id = 2, 
                        Name = "Dry Cleaning", 
                        Description = "Premium dry cleaning service for delicate and professional garments.", 
                        PricePerKg = 45.00m, 
                        MinimumWeightKg = 1, 
                        IsAvailable = true 
                    },
                    new Service 
                    { 
                        Id = 3, 
                        Name = "Ironing Service", 
                        Description = "Professional ironing to keep your clothes crisp and wrinkle-free.", 
                        PricePerKg = 15.00m, 
                        MinimumWeightKg = 3, 
                        IsAvailable = true 
                    },
                    new Service 
                    { 
                        Id = 4, 
                        Name = "Bedding & Linens", 
                        Description = "Specialized cleaning for bedding, curtains, and household linens.", 
                        PricePerKg = 35.00m, 
                        MinimumWeightKg = 5, 
                        IsAvailable = true 
                    }
                };
            }
        }

        // GET: Services
        public ActionResult Index()
        {
            return View("~/Views/Home/Services.cshtml", _mockServices);
        }

        // GET: Services/Admin
        public ActionResult Admin()
        {
            return View(_mockServices);
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = _mockServices.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,PricePerKg,MinimumWeightKg,ImageUrl,IsAvailable")] Service service)
        {
            if (ModelState.IsValid)
            {
                service.Id = _mockServices.Any() ? _mockServices.Max(s => s.Id) + 1 : 1;
                _mockServices.Add(service);
                return RedirectToAction("Admin");
            }

            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = _mockServices.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,PricePerKg,MinimumWeightKg,ImageUrl,IsAvailable")] Service service)
        {
            if (ModelState.IsValid)
            {
                var existingService = _mockServices.FirstOrDefault(s => s.Id == service.Id);
                if (existingService != null)
                {
                    existingService.Name = service.Name;
                    existingService.Description = service.Description;
                    existingService.PricePerKg = service.PricePerKg;
                    existingService.MinimumWeightKg = service.MinimumWeightKg;
                    existingService.ImageUrl = service.ImageUrl;
                    existingService.IsAvailable = service.IsAvailable;
                }
                return RedirectToAction("Admin");
            }
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = _mockServices.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = _mockServices.FirstOrDefault(s => s.Id == id);
            if (service != null)
            {
                _mockServices.Remove(service);
            }
            return RedirectToAction("Admin");
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