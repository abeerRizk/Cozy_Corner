using ITI_Project.BLL.ModelVM;
using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Interface
{
    public interface IProductService
    {
        public Task< bool> Create(CreateProductVM product);
        public Task <bool> Update(UpdateProductVM product);
        public Task <bool> Delete(int id);
        public Task< IEnumerable<GetProductVM>> GetAll();

        public Task <GetProductVM> GetByProductId(int? id);


        public Task<List<Product>> GetFavoriteProductsByCustomerId(int customerId);

    }
}
