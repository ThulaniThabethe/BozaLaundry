extern alias EF;
using EF::System.Data.Entity;
using EF::System.Data.Entity.Infrastructure;
using EF::System.Data.Entity.Migrations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("BozLaundryEntities", throwIfV1Schema: false)
        {
        }

        public EF::System.Data.Entity.DbSet<Order> Orders { get; set; }
        public EF::System.Data.Entity.DbSet<OrderItem> OrderItems { get; set; }
        public EF::System.Data.Entity.DbSet<Service> Services { get; set; }
        public EF::System.Data.Entity.DbSet<ServiceType> ServiceTypes { get; set; }
        public EF::System.Data.Entity.DbSet<OrderStatus> OrderStatuses { get; set; }
        public EF::System.Data.Entity.DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public EF::System.Data.Entity.DbSet<InventoryItem> InventoryItems { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}