using AutoMapper;
using ITI_Project.BLL.Helper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Impelemntation;
using ITI_Project.DAL.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Impelemntation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo customerRepo;
        private readonly IOrderRepo orderRepo;
        private readonly IMapper mapper;

        public CustomerService(ICustomerRepo customerRepo, IMapper mapper)
        {
            this.customerRepo = customerRepo;
            this.mapper = mapper;
        }

        public async Task< (bool sucess, string message)> Create(CreateCustomerVM customer)
        {
            try
            {
                Customer new_customer = mapper.Map<Customer>(customer);
                if (await customerRepo.IsEmailExist(new_customer.Email) )
                    return (false, "Email is already exist");
                await customerRepo.Create(new_customer);
                return (true , string.Empty);
            }
            catch (Exception e)
            {
                return (false , e.Message);
            }
        }


        public async Task< IEnumerable<GetCustomerVM>> GetAll()
        {
            var result = await customerRepo.GetAll();
            List<GetCustomerVM> newResult = mapper.Map<List<GetCustomerVM>>(result);
            return newResult;
        }

        public async Task< GetCustomerVM> GetByCustomerId(int id)
        {
            Customer customer = await customerRepo.GetByCustomerId(id);
            return mapper.Map<GetCustomerVM>(customer);
        }

        public async Task<bool> Update(UpdateCustomerVM customer)
        {
            try
            {
                Customer new_customer = mapper.Map<Customer>(customer);
                await customerRepo.Update(new_customer);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public  async Task< int >GetCustomerId_ByUserId(string userId)
        {
            return await customerRepo.GetCustomerId_ByUserId(userId);
        }








    }
}
