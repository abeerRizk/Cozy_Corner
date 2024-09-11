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
        Order GetOrderById(int id);
        IEnumerable<Order> GetAllOrders();
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
        public void AddOrderItem(int id, Product item);
        public void RemoveItem(int id , Product item);
    }
}
