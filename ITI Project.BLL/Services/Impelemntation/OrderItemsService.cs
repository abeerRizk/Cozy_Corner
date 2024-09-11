using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Repo.Interface;
using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Impelemntation
{
    public class OrderItemsService : IOrderItemsService
    {
        private readonly IOrderItemRepo orderItemRepo;
        private readonly IMapper mapper;
        public OrderItemsService(IOrderItemRepo orderItemRepo , IMapper mapper )
        {
            this.orderItemRepo = orderItemRepo;
            this.mapper = mapper;
        }

        public void Create(OrderItemsVM orderItems)
        {
            var data = mapper.Map<OrderItem>( orderItems);
            orderItemRepo.Create( data );
        }

        public void Delete(int id)
        {
            orderItemRepo.Delete(id);
        }

        public OrderItemsVM GetById(int id)
        {
            var item = orderItemRepo.GetById(id);
            return mapper.Map<OrderItemsVM>(item);
        }

        public IEnumerable<OrderItemsVM> GetAll()
        {
            var data = orderItemRepo.GetAll();
            return mapper.Map<IEnumerable<OrderItemsVM>>(data);
        }

        public bool Update(OrderItemsVM orderItems)
        {
            var data = mapper.Map<OrderItem>(orderItems);
            return orderItemRepo.Update(data);
        }
    }
}
