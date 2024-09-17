using ITI_Project.DAL.DB.ApplicationDB;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> Create(Customer customer)
        {
            try
            {
                await db.Customers.AddAsync(customer);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public async Task<List<Customer>> GetAll()
        {
            var result = await db.Customers.Where(a=>a.IsDeleted != true).ToListAsync();
            return result;
        }
        public async Task<List<Customer>> GetAllForGmail()
        {
            return  await db.Customers.Where(c => c.Email.EndsWith("@gmail.com")).ToListAsync();
        }

        public async Task< Customer> GetByCustomerId(int id)
        {
            var data =  await db.Customers.Where(a => a.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<bool> Update(Customer customer)
        {
            try
            {
                var data = await db.Customers.FirstOrDefaultAsync(t=>t.Id==customer.Id);

                data.Age = customer.Age;
                data.Name = customer.Name;
                data.Phone_Number = customer.Phone_Number;
                data.Location = customer.Location;
                data.hasOrder = customer.hasOrder;

                try
                {
                  await  db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving changes: {ex.Message}");
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> IsEmailExist(string email)
        {
            return await db.Customers.AnyAsync(a => a.Email == email);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
        public async Task <int> GetCustomerId_ByUserId(string userId)
        {
            var data = await db.Customers.FirstOrDefaultAsync(a => a.userId == userId);
            return data.Id;

        }
    }
}
