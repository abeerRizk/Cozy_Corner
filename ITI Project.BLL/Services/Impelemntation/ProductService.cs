using AutoMapper;
using ITI_Project.BLL.Helper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Impelemntation;
using ITI_Project.DAL.Repo.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Impelemntation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo ProductRepo;
        private readonly IMapper mapper;

        public ProductService(IProductRepo ProductRepo, IMapper mapper)
        {
            this.ProductRepo = ProductRepo;
            this.mapper = mapper;
        }
        public async  Task <bool> Create(CreateProductVM product)
        {
            try
            {
                Product new_product = mapper.Map<Product>(product);
                
                await ProductRepo.Create(new_product);
                return true;
            }
            catch (Exception e)
            {
                // Log the exception (optional)
                return false;
            }
        }


        public async Task< bool> Delete(int id)
        {
            try
            {

                await ProductRepo.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async  Task<IEnumerable<GetProductVM>> GetAll()
        {
            var result = await ProductRepo.GetAll();
            List<GetProductVM> newResult = mapper.Map<List<GetProductVM>>(result);
            return newResult;
        }



        public async Task<GetProductVM> GetByProductId(int? id)
        {
            Product product = await ProductRepo.GetByProductId(id);
            return mapper.Map<GetProductVM>(product);
        }



        public async Task< bool> Update(UpdateProductVM product)
        {
            try
            {
                Product new_product = mapper.Map<Product>(product);

               await  ProductRepo.Update(new_product);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public async Task<List<Product>> GetFavoriteProductsByCustomerId(int customerId)
        {
            return  await ProductRepo.GetFavoriteProductsByCustomerId(customerId);
        }


    }
}
