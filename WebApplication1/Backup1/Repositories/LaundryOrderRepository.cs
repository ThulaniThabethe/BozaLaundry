extern alias EF;
using WebApplication1.Models;
using EF::System.Data.Entity;

namespace WebApplication1.Repositories
{
    public class LaundryOrderRepository : Repository<LaundryOrder>
    {
        public LaundryOrderRepository(DbContext context) : base(context)
        {
        }
    }
}