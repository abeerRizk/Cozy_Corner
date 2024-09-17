using ITI_Project.DAL.DB.ApplicationDB;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ITI_Project.DAL.Repo.Impelemntation
{
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDbContext db;

        public ProductRepo(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        public async Task<bool> Create(Product product)
        {
            try
            {
                product.Available = true;
                await db.Products.AddAsync(product);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var data =  await db.Products.FirstOrDefaultAsync(a => a.Id == id);
                if (data.IsDeleted)
                {
                    throw new Exception("The Product is already deleted");

                }
              
          
                data.IsDeleted = true;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Product>> GetAll()
        {
            var result = await db.Products.Where(a => a.IsDeleted != true && a.Available == true).ToListAsync();
            return result;
        }


        public async Task<Product> GetByProductId(int? id)
        {
            var data = await db.Products.Where(a => a.Id == id).FirstOrDefaultAsync();
            return data;
        }
        public async Task< bool> Update(Product product)
        {
            try
            {
                var data =  await db.Products.Where(a => a.Id == product.Id).FirstOrDefaultAsync();

                data.Name = product.Name;
                
                data.Description = product.Description;
                data.Price = product.Price;
                data.Category = product.Category;
                data.Quantity = product.Quantity;
                if(data.Quantity == 0)
                {
                    data.Available = false;
                }
                else
                {
                    data.Available = true;
                }
                data.Images = product.Images;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task< List<Product>> GetFavoriteProductsByCustomerId(int customerId)
        {
            
            return await db.Favorites.Where(f => f.CutomerId == customerId && f.IsActive == true  )
                .Include(f => f.Product).Select(f => f.Product).ToListAsync();
        }
    }
}
