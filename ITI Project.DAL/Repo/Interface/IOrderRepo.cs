using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Interface
{
    public interface IOrderRepo
    {
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetAllOrders();
        Task AddOrderItem(int customerId, OrderItem item);
        Task RemoveOrderItem(int customerId, OrderItem item);
        Task UpdateOrder(Order order);
        Task UpdateOrderStatus(Order order);
        Task DeleteOrder(int id);
        Task DeleteUnconfirmedOrders();
    }
}
