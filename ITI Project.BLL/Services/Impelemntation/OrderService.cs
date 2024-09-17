using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Impelemntation;
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
        private readonly IINvoiceRepo invoiceRepo;

        public OrderService(IOrderRepo orderRepository, IMapper mapper , IINvoiceRepo invoiceRepo)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            this.invoiceRepo = invoiceRepo ?? throw new ArgumentNullException(nameof(invoiceRepo)); 
        }

        public async Task < OrderModelVM> GetOrderById(int id)
        {
            var order =  await _orderRepository.GetOrderById(id);
            return _mapper.Map<OrderModelVM>(order);
        }

        public async Task< IEnumerable<OrderModelVM>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrders();
            return _mapper.Map<IEnumerable<OrderModelVM>>(orders);
        }




        public async Task AddOrderItem(int CustomerId , OrderItemsVM item)
        {
            OrderItem new_orderItem = _mapper.Map<OrderItem>(item);


            _orderRepository.AddOrderItem(CustomerId, new_orderItem);
        }

        public async Task RemoveOrderItem(int CustomerId, OrderItemsVM item)
        {
            OrderItem new_orderItem = _mapper.Map<OrderItem>(item);


            _orderRepository.RemoveOrderItem(CustomerId, new_orderItem);
        }

        public async Task UpdateOrder(OrderModelVM orderViewModel)
        {
            var order = _mapper.Map<Order>(orderViewModel);
              _orderRepository.UpdateOrder(order);
        }
        public async Task UpdateOrderStatus(OrderModelVM orderViewModel)
        {
            var order = _mapper.Map<Order>(orderViewModel);
            await  _orderRepository.UpdateOrderStatus(order);
        }

        public async Task DeleteOrder(int id)
        {
            _orderRepository.DeleteOrder(id);
        }
        public void DeleteUnconfirmedOrders()
        {
            _orderRepository.DeleteUnconfirmedOrders();
        }

    }
}
