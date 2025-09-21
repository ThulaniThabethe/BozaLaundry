extern alias EF;
extern alias SCD;
using System;

namespace WebApplication1.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Name { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.StringLength(500)]
        public string Description { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Unit { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.Range(0, int.MaxValue)]
        public int LowStockThreshold { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.DataType(SCD::System.ComponentModel.DataAnnotations.DataType.Date)]
        [SCD::System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdated { get; set; }
    }
}