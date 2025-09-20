using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ReportViewModel
    {
        public List<OrderTrend> OrderTrends { get; set; }
        public decimal CustomerRetentionRate { get; set; }
        public RevenueReportViewModel RevenueReport { get; set; }
    }

    public class OrderTrend
    {
        public System.DateTime Date { get; set; }
        public int OrderCount { get; set; }
    }
}