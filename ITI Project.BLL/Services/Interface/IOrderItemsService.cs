using ITI_Project.BLL.ModelVM;
using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Interface
{
    public interface IOrderItemsService
    {

        public  Task<OrderItemsVM> GetById(int id);
        public Task< IEnumerable<OrderItemsVM>> GetAll(int OrderId , int vendorId);
        public  Task Delete(int itemId);


    }
}
