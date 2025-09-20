using System.Web.Mvc;
using WebApplication1.Models;
using System.Linq;
using System.Collections.Generic;
using System;

namespace WebApplication1.Controllers
{
    public class ReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Report
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.CustomerProfile).ToList();

            // Calculate Order Trend
            var orderTrends = orders.GroupBy(o => o.OrderDate.Date)
                                    .Select(g => new OrderTrend { Date = g.Key, OrderCount = g.Count() })
                                    .OrderBy(x => x.Date)
                                    .ToList();

            // Calculate Customer Retention Rate
            var allCustomers = db.CustomerProfiles.Select(c => c.CustomerProfileId).Distinct().ToList();
            var returningCustomers = db.Orders.GroupBy(o => o.CustomerProfileId)
                                            .Where(g => g.Count() > 1)
                                            .Select(g => g.Key)
                                            .ToList();
            decimal customerRetentionRate = allCustomers.Any() ? (decimal)returningCustomers.Count / allCustomers.Count * 100 : 0;

            // Calculate Daily Revenue
            var dailyRevenues = orders.GroupBy(o => o.OrderDate.Date)
                                      .Select(g => new DailyRevenue { Date = g.Key, TotalRevenue = g.Sum(o => o.TotalAmount) })
                                      .OrderBy(x => x.Date)
                                      .ToList();

            // Calculate Weekly Revenue
            var weeklyRevenues = orders.GroupBy(o => new { Year = o.OrderDate.Year, Week = System.Data.Entity.DbFunctions.TruncateTime(o.OrderDate).Value.DayOfYear / 7 })
                                       .Select(g => new WeeklyRevenue { Year = g.Key.Year, WeekNumber = g.Key.Week, TotalRevenue = g.Sum(o => o.TotalAmount) })
                                       .OrderBy(x => x.Year).ThenBy(x => x.WeekNumber)
                                       .ToList();

            var viewModel = new ReportViewModel
            {
                OrderTrends = orderTrends,
                CustomerRetentionRate = customerRetentionRate,
                RevenueReport = new RevenueReportViewModel
                {
                    DailyRevenues = dailyRevenues,
                    WeeklyRevenues = weeklyRevenues
                }
            };

            return View(viewModel);
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