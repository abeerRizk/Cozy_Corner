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
        public bool AddVendor(Vendor vendor)
        {
            try
            {
                
                db.Vendor.Add(vendor);
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;

            }
        }

        public bool DeleteVendor(int id)
        {
            try
            {
                var ven = db.Vendor.FirstOrDefault(e => e.Id == id);
                if (ven != null)
                {
                    ven.IsDeleted = !ven.IsDeleted;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Vendor> GetAllVendors()
        {
            return db.Vendor.ToList();
        }

        public Vendor GetVendorById(int id)
        {
            return db.Vendor.FirstOrDefault(v => v.Id == id);
        }

        public bool UpdateVendor(Vendor vendor)
        {
            try
            {
                var ven = db.Vendor.Where(a => a.Id == vendor.Id).FirstOrDefault();
                ven.Age = vendor.Age;
                ven.Name = vendor.Name;
                ven.Phone_Number = vendor.Phone_Number;
                ven.Location = vendor.Location;

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
            return db.Vendor.Any(a => a.Email == email);
        }
    }
}
