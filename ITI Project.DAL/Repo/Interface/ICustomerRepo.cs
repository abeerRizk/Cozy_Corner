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
        public bool Create(Customer customer);
        public bool Update(Customer customer);
        public bool Delete(int id);
        public List<Customer> GetAll();

        public Customer GetByCustomerId(int id);
        public bool IsEmailExist(string email);
        public void SaveChanges();
    }
}
