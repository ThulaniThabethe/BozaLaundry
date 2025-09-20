using WebApplication1.Models;
using System.Data.Entity;

namespace WebApplication1.Repositories
{
    public class LaundryOrderRepository : Repository<LaundryOrder>
    {
        public LaundryOrderRepository(DbContext context) : base(context)
        {
        }
    }
}