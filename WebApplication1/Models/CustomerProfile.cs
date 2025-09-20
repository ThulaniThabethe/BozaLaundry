using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CustomerProfile
    {
        [Key]
        public string CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(256)]
        public string Address { get; set; }

        public DateTime RegistrationDate { get; set; }

        // Navigation property for the associated ApplicationUser
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Customer")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        // Navigation property for orders placed by this customer
        public virtual ICollection<Order> Orders { get; set; }
    }
}