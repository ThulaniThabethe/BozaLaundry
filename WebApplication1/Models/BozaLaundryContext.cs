using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebApplication1.Models
{
    public class BozaLaundryContext : DbContext
    {
        public BozaLaundryContext() : base("BozaLaundryContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<LaundryOrder> LaundryOrders { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}