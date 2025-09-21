using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            try
            {
                var dashboardData = new
                {
                    TotalCustomers = db.CustomerProfiles?.Count() ?? 0,
                    TotalOrders = db.Orders?.Count() ?? 0,
                    PendingOrders = db.Orders?.Where(o => o.OrderStatus != null && o.OrderStatus.StatusName == "Pending").Count() ?? 0,
                    TotalServices = db.Services?.Count() ?? 0,
                    RecentOrders = db.Orders?.OrderByDescending(o => o.OrderDate).Take(5).ToList() ?? new List<Order>()
                };
                
                ViewBag.DashboardData = dashboardData;
                return View();
            }
            catch (Exception)
            {
                // Log the error and return a safe default view
                var safeDashboardData = new
                {
                    TotalCustomers = 0,
                    TotalOrders = 0,
                    PendingOrders = 0,
                    TotalServices = 0,
                    RecentOrders = new List<Order>()
                };
                
                ViewBag.DashboardData = safeDashboardData;
                ViewBag.ErrorMessage = "Data temporarily unavailable. Please try again later.";
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Professional Laundry Services You Can Trust";
            ViewBag.CompanyName = "BozaLaundry";
            ViewBag.FoundedYear = "2020";
            ViewBag.ServicesCount = 8;
            ViewBag.CustomersServed = "5000+";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Get in Touch with BozaLaundry";
            ViewBag.Phone = "+27 11 123 4567";
            ViewBag.Email = "info@bozalaundry.co.za";
            ViewBag.Address = "123 Bree Street, Johannesburg, Gauteng 2001";
            ViewBag.Hours = "Mon-Fri: 8AM-6PM, Sat: 9AM-4PM, Sun: Closed";

            return View();
        }

        public ActionResult Services()
        {
            // Temporarily use mock data to bypass database issues
            var services = GetMockServices();
            return View(services);
        }
        
        private List<Service> GetMockServices()
        {
            return new List<Service>
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