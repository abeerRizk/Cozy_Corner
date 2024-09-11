using ITI_Project.DAL.DB.ApplicationDB;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Impelemntation
{
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDbContext db;

        public ProductRepo(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        public bool Create(Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = db.Products.Where(a => a.Id == id).FirstOrDefault();
                if (data.IsDeleted)
                {
                    throw new Exception("The Product is already deleted");

                }
                data.IsDeleted = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> GetAll()
        {
            var result = db.Products.ToList();
            return result;
        }

        public List<Product> GetByCategory(string Category)
        {
            var data = db.Products.Where(a => a.Category == Category).ToList();
            return data;
        }

        public Product GetByProductId(int id)
        {
            var data = db.Products.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }

        public List<Product> GetByVendor(int VendorId)
        {
            var data = db.Products.Where(a => a.VendorID == VendorId).ToList();
            return data;
        }

        public List<Product> GetByVendorAndCategory(string Category, int VendorId)
        {
            var data = db.Products.Where(a => a.VendorID ==VendorId && a.Category == Category).ToList();
            return data;
        }

        public IEnumerable<Product> GetProductsBySearchAndCategory(string searchTerm, string category)
        {
            var query = db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }

        public bool Update(Product product)
        {
            try
            {
                var data = db.Products.Where(a => a.Id == product.Id).FirstOrDefault();
                data.Name = product.Name;
                data.Available = product.Available;
                data.Description = product.Description;
                data.Price = product.Price;
                data.Category = product.Category;
                data.Quantity = product.Quantity;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
