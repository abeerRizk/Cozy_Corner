using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Interface
{
    public interface ICustomerRepo
    {
        public Task <bool> Create(Customer customer);
        public Task<bool> Update(Customer customer);
        public Task <List<Customer>> GetAll();
        public  Task<Customer> GetByCustomerId(int id);
        public Task<bool> IsEmailExist(string email);

        public Task<int> GetCustomerId_ByUserId(string userId);
        public Task< List<Customer>> GetAllForGmail();
    }
}
