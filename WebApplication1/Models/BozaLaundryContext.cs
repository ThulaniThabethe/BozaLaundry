using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class BozaLaundryContext : DbContext
    {
        public BozaLaundryContext() : base("BozaLaundryContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<LaundryOrder> LaundryOrders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to use a different table name for the Customer class
            // modelBuilder.Entity<Customer>().ToTable("Customers");
            // modelBuilder.Entity<LaundryOrder>().ToTable("LaundryOrders");
        }
    }
}