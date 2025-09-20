using System.Data.Entity;
using WebApplication1.Models;
using System.Linq;
using WebApplication1.App_Start;


namespace WebApplication1.App_Start
{
    public class DbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            // Seed Order Statuses
            if (!context.OrderStatuses.Any())
            {
                context.OrderStatuses.Add(new OrderStatus { StatusName = "Pending", Description = "Order has been placed and is awaiting processing." });
                context.OrderStatuses.Add(new OrderStatus { StatusName = "Processing", Description = "Order is being processed." });
                context.OrderStatuses.Add(new OrderStatus { StatusName = "In Laundry", Description = "Items are currently being laundered." });
                context.OrderStatuses.Add(new OrderStatus { StatusName = "Ready for Pickup", Description = "Order is complete and ready for customer pickup." });
                context.OrderStatuses.Add(new OrderStatus { StatusName = "Out for Delivery", Description = "Order is out for delivery." });
                context.OrderStatuses.Add(new OrderStatus { StatusName = "Delivered", Description = "Order has been successfully delivered." });
                context.OrderStatuses.Add(new OrderStatus { StatusName = "Cancelled", Description = "Order has been cancelled." });
                context.SaveChanges();
            }

            // Seed Service Types
            if (!context.ServiceTypes.Any())
            {
                context.ServiceTypes.Add(new ServiceType { Name = "Wash & Fold", Description = "Standard wash, dry, and fold service.", PricePerUnit = 1.50m, Unit = "lb" });
                context.ServiceTypes.Add(new ServiceType { Name = "Dry Cleaning", Description = "Professional dry cleaning for delicate items.", PricePerUnit = 5.00m, Unit = "item" });
                context.ServiceTypes.Add(new ServiceType { Name = "Ironing", Description = "Ironing service for wrinkle-free clothes.", PricePerUnit = 2.00m, Unit = "item" });
                context.SaveChanges();
            }

            base.Seed(context);
        }
    }
}