using WebApplication1.Models;
using System.Data.Entity;

namespace WebApplication1.Repositories
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(DbContext context) : base(context)
        {
        }
    }
}