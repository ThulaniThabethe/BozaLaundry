extern alias EF;
extern alias SCD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCD::System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public enum LaundryOrderStatus
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
        [EF::System.ComponentModel.DataAnnotations.Schema.ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        public string ServiceType { get; set; } // e.g., Wash, Dry, Iron, Dry Cleaning

        public double? Weight { get; set; } // in kg or lbs

        public int? ItemCount { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.DataType(SCD::System.ComponentModel.DataAnnotations.DataType.Date)]
        [SCD::System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.DataType(SCD::System.ComponentModel.DataAnnotations.DataType.Date)]
        [SCD::System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PickupDate { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.DataType(SCD::System.ComponentModel.DataAnnotations.DataType.Date)]
        [SCD::System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DeliveryDate { get; set; }

        public LaundryOrderStatus Status { get; set; }

        [EF::System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        [EF::System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        public string Notes { get; set; }
    }
}