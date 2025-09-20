using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public enum OrderStatus
    {
        Pending,
        InProgress,
        ReadyForPickup,
        Completed,
        Cancelled
    }

    public class LaundryOrder
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [Required]
        public string ServiceType { get; set; } // e.g., Wash, Dry, Iron, Dry Cleaning

        public double? Weight { get; set; } // in kg or lbs

        public int? ItemCount { get; set; }

        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PickupDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DeliveryDate { get; set; }

        public OrderStatus Status { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }

        public string Notes { get; set; }
    }
}