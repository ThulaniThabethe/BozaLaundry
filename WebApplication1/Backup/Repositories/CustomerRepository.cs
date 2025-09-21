extern alias EF;
using WebApplication1.Models;
using EF::System.Data.Entity;

namespace WebApplication1.Repositories
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(DbContext context) : base(context)
        {
        }
    }
}