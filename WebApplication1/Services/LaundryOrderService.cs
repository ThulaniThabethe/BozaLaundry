using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class LaundryOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LaundryOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<LaundryOrder> GetAllLaundryOrders()
        {
            return _unitOfWork.LaundryOrders.GetAll().ToList();
        }

        public LaundryOrder GetLaundryOrderById(int id)
        {
            return _unitOfWork.LaundryOrders.GetById(id);
        }

        public void AddLaundryOrder(LaundryOrder order)
        {
            _unitOfWork.LaundryOrders.Add(order);
            _unitOfWork.Complete();
        }

        public void UpdateLaundryOrder(LaundryOrder order)
        {
            _unitOfWork.LaundryOrders.Update(order);
            _unitOfWork.Complete();
        }

        public void DeleteLaundryOrder(int id)
        {
            var order = _unitOfWork.LaundryOrders.GetById(id);
            if (order != null)
            {
                _unitOfWork.LaundryOrders.Remove(order);
                _unitOfWork.Complete();
            }
        }
    }
}