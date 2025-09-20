using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? PickupDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        [Required]
        public string Status { get; set; } // e.g., Pending, In Progress, Ready for Pickup, Delivered

        public ICollection<OrderItem> OrderItems { get; set; }

        public decimal TotalAmount { get; set; }
    }
}