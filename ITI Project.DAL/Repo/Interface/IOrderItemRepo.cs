using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Interface
{
    public interface IOrderItemRepo
    {
        public void Create(OrderItem item);
        public bool Update(OrderItem item);
        public void Delete(int id);
        public OrderItem GetById(int id);
        public IEnumerable<OrderItem> GetAll();
    }
}
