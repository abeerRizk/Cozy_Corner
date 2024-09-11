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

        public void ConfirmOrder(int OrderId)
        {
            Order order = _orderRepository.GetOrderById(OrderId);
            Invoice invoice = new Invoice();
            invoice.OrderId = OrderId;
            invoice.Order = order;
            invoice.TotallPrice = order.TotalPrice;
            invoice.IsPaid = false;

           invoiceRepo.Create(invoice);
        }

    }
}
