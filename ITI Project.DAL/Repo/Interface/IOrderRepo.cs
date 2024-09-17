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
        public  Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetAllOrders();

        public void AddOrderItem(int customerId, OrderItem item);

        public void RemoveOrderItem(int customerId, OrderItem item);
        public void UpdateOrder(Order order);
        Task UpdateOrderStatus(Order order);
        public void DeleteOrder(int id);
    }
}
