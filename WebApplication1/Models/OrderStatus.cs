using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string StatusName { get; set; } // e.g., Pending, In Progress, Completed, Delivered

        [StringLength(250)]
        public string Description { get; set; }
    }
}