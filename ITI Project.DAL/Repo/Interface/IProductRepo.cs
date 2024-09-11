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
        public bool Create(Product product);
        public bool Update(Product product);
        public bool Delete(int id);
        public List<Product> GetAll();
        public Product GetByProductId(int id);
        public List<Product> GetByCategory(string Category);
        public List<Product> GetByVendor(int VendorId);
        public List<Product> GetByVendorAndCategory(string Category , int VendorId);
        IEnumerable<Product> GetProductsBySearchAndCategory(string searchTerm, string category);



    }
}
