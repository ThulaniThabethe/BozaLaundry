using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, 1000.00)]
        public decimal PricePerKg { get; set; }

        [Range(0, 100)]
        public int MinimumWeightKg { get; set; }

        public bool IsAvailable { get; set; }
    }
}