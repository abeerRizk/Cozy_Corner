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



        public async Task < OrderItemsVM> GetById(int id)
        {
            var item =await  orderItemRepo.GetById(id);
            return mapper.Map<OrderItemsVM>(item);
        }

        public async Task <IEnumerable<OrderItemsVM>> GetAll(int OrderId , int vendorId)
        {
            var data = await orderItemRepo.GetAll(OrderId , vendorId);
            return mapper.Map<IEnumerable<OrderItemsVM>>(data);
        }

        public async Task Delete(int itemId)
        {
            await orderItemRepo.Delete(itemId);
        }
    }
}
