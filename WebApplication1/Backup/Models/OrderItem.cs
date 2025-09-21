using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        public decimal PricePerUnit { get; set; }
    }
}