using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ServiceType
    {
        [Key]
        public int ServiceTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal PricePerUnit { get; set; } // e.g., price per kg, or per item

        [Required]
        [StringLength(50)]
        public string Unit { get; set; } // e.g., kg, item, load

        public decimal? MinWeight { get; set; }
        public decimal? MaxWeight { get; set; }
        public int? MinItems { get; set; }
        public int? MaxItems { get; set; }
        public decimal? BundlePrice { get; set; }
    }
}