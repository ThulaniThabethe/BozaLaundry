using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class RevenueReportViewModel
    {
        public List<DailyRevenue> DailyRevenues { get; set; }
        public List<WeeklyRevenue> WeeklyRevenues { get; set; }
    }

    public class DailyRevenue
    {
        public DateTime Date { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class WeeklyRevenue
    {
        public int Year { get; set; }
        public int WeekNumber { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}