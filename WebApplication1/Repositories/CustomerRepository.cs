using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(DbContext context) : base(context)
        {
        }
    }
}