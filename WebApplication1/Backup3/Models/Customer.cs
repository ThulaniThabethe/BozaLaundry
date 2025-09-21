extern alias EF;
extern alias SCD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.StringLength(100)]
        public string FirstName { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.StringLength(100)]
        public string LastName { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.StringLength(200)]
        public string Address { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.StringLength(20)]
        [SCD::System.ComponentModel.DataAnnotations.Phone]
        public string PhoneNumber { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.StringLength(100)]
        [SCD::System.ComponentModel.DataAnnotations.EmailAddress]
        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; }

        // Navigation property for orders
        public virtual ICollection<LaundryOrder> LaundryOrders { get; set; }
    }
}