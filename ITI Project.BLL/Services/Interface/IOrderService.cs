using ITI_Project.BLL.ModelVM;
using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Interface
{
    public interface IOrderService
    {
        public Task<OrderModelVM> GetOrderById(int id);
         Task <IEnumerable<OrderModelVM>> GetAllOrders();


        public Task AddOrderItem(int CustomerId, OrderItemsVM item);
        public Task RemoveOrderItem(int CustomerId, OrderItemsVM item);

        public Task UpdateOrder(OrderModelVM orderViewModel);
        Task UpdateOrderStatus(OrderModelVM orderViewModel);
        public Task DeleteOrder(int id);

    }
}
