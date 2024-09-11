using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Impelemntation
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepo _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepo orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public OrderModelVM GetOrderById(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            return _mapper.Map<OrderModelVM>(order);
        }

        public IEnumerable<OrderModelVM> GetAllOrders()
        {
            var orders = _orderRepository.GetAllOrders();
            return _mapper.Map<IEnumerable<OrderModelVM>>(orders);
        }

        public void AddOrder(OrderModelVM orderViewModel)
        {
            var order = _mapper.Map<Order>(orderViewModel);
            _orderRepository.AddOrder(order);
        }

        public void UpdateOrder(OrderModelVM orderViewModel)
        {
            var order = _mapper.Map<Order>(orderViewModel);
            _orderRepository.UpdateOrder(order);
        }

        public void DeleteOrder(int id)
        {
            _orderRepository.DeleteOrder(id);
        }
    }
}
