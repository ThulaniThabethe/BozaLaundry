using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BozaLaundryContext _context;

        public CustomerRepository Customers { get; private set; }
        public LaundryOrderRepository LaundryOrders { get; private set; }

        public UnitOfWork(BozaLaundryContext context)
        {
            _context = context;
            Customers = new CustomerRepository(_context);
            LaundryOrders = new LaundryOrderRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}