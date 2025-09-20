using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class CustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _unitOfWork.Customers.GetAll().ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _unitOfWork.Customers.GetById(id);
        }

        public void AddCustomer(Customer customer)
        {
            _unitOfWork.Customers.Add(customer);
            _unitOfWork.Complete();
        }

        public void UpdateCustomer(Customer customer)
        {
            _unitOfWork.Customers.Update(customer);
            _unitOfWork.Complete();
        }

        public void DeleteCustomer(int id)
        {
            var customer = _unitOfWork.Customers.GetById(id);
            if (customer != null)
            {
                _unitOfWork.Customers.Remove(customer);
                _unitOfWork.Complete();
            }
        }
    }
}