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
        public bool Create(CreateProductVM product);
        public bool Update(UpdateProductVM product);
        public bool Delete(int id);
        public IEnumerable<GetProductVM> GetAll();

        public GetProductVM GetByProductId(int id);

        public List<GetProductVM> GetByCategory(string Category);
        public List<GetProductVM> GetByVendor(int VendorId);
        public List<GetProductVM> GetByVendorAndCategory(string Category, int VendorId);
        
    }
}
