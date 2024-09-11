using ITI_Project.DAL.DB.ApplicationDB;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Impelemntation
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly ApplicationDbContext db;

        public CustomerRepo(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        public bool Create(Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
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
                var data = db.Customers.Where(a => a.Id == id).FirstOrDefault();
                if (data.IsDeleted == true)
                {
                    throw new Exception("The Customer is already deleted");

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

        public List<Customer> GetAll()
        {
            var result = db.Customers.ToList();
            return result;
        }

        public Customer GetByCustomerId(int id)
        {
            var data = db.Customers.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }

        public bool Update(Customer customer)
        {
            try
            {
                var data = db.Customers.Where(a => a.Id == customer.Id).FirstOrDefault();

                data.Age = customer.Age;
                data.Name = customer.Name;
                data.Phone_Number = customer.Phone_Number;
                data.Location = customer.Location;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool IsEmailExist(string email)
        {
            return db.Customers.Any(a => a.Email == email);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

    }
}
