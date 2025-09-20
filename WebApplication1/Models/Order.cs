using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual CustomerProfile CustomerProfile { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? PickupDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual OrderStatus OrderStatus { get; set; }

        public int ServiceTypeId { get; set; }
        [ForeignKey("ServiceTypeId")]
        public virtual ServiceType ServiceType { get; set; }

        public double? Weight { get; set; }

        public string SpecialInstructions { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        public string InvoiceNumber { get; set; }
        public DateTime? ReceiptGeneratedDate { get; set; }
        public bool IsInvoiceGenerated { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}