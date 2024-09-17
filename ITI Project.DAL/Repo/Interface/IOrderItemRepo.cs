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
 
        public Task<OrderItem> GetById(int id);
        public Task<IEnumerable<OrderItem>> GetAll();
    }
}
