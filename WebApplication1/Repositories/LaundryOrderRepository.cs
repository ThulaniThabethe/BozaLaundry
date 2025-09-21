using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class LaundryOrderRepository : Repository<LaundryOrder>
    {
        public LaundryOrderRepository(DbContext context) : base(context)
        {
        }
    }
}