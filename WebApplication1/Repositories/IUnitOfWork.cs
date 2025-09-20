using System;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        CustomerRepository Customers { get; }
        LaundryOrderRepository LaundryOrders { get; }
        int Complete();
    }
}