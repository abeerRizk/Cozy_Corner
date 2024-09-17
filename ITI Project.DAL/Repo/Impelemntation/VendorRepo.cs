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
    public class VendorRepo : IVendorRepo
    {
        private readonly ApplicationDbContext db;

        public VendorRepo(ApplicationDbContext context)
        {
            db = context;
        }
        public async Task<bool> AddVendor(Vendor vendor)
        {
            try
            {
                
                await db.Vendor.AddAsync(vendor);
                await db.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                return false;

            }
        }

        public async Task <bool> DeleteVendor(int id)
        {
            try
            {
                var ven = await db.Vendor.FirstOrDefaultAsync(e => e.Id == id);
                if (ven != null)
                {
                    ven.IsDeleted = !ven.IsDeleted;
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task< IEnumerable<Vendor>> GetAllVendors()
        {
            return await db.Vendor.Where(a => a.IsDeleted != true).ToListAsync();
        }

        public async Task <Vendor> GetVendorById(int id)
        {
            var data =  await db.Vendor.FirstOrDefaultAsync(v => v.Id == id);
            return data;
        }

        public  async Task< bool> UpdateVendor(Vendor vendor)
        {
            try
            {
                var ven = await db.Vendor.FirstOrDefaultAsync(a => a.Id == vendor.Id);
                ven.Age = vendor.Age;
                ven.Name = vendor.Name;
                ven.Phone_Number = vendor.Phone_Number;
                ven.Location = vendor.Location;

                await db.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task <bool> IsEmailExist(string email)
        {
            return await db.Vendor.AnyAsync(a => a.Email == email);
        }
        public async Task< int> GetVendorId_ByUserId(string userId)
        {
            var vendor = await db.Vendor.FirstOrDefaultAsync(a => a.userId == userId);
             return vendor.Id;
            

           
        }

    }
}
