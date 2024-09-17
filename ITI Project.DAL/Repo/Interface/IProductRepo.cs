using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Interface
{
    public interface IProductRepo
    {
        public Task<bool> Create(Product product);
        public Task<bool> Update(Product product);
        public Task< bool> Delete(int id);
        public Task< List<Product>> GetAll();
        public Task< Product> GetByProductId(int? id);
  
        Task<List<Product>> GetFavoriteProductsByCustomerId(int customerId);


    }
}
