using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(20)]
        [Phone]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; }

        // Navigation property for orders
        public virtual ICollection<LaundryOrder> LaundryOrders { get; set; }
    }
}