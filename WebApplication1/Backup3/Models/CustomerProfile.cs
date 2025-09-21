extern alias EF;
extern alias SCD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CustomerProfile
    {
        [SCD::System.ComponentModel.DataAnnotations.Key]
        public string CustomerId { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.StringLength(100)]
        public string FullName { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.EmailAddress]
        [SCD::System.ComponentModel.DataAnnotations.StringLength(256)]
        public string Email { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.StringLength(20)]
        public string PhoneNumber { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}