using ITI_Project.DAL.DB.ApplicationDB;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Impelemntation
{
    public class FollowRepo : IFollowRepo
    {
        private readonly ApplicationDbContext db;

        public FollowRepo(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }


        public void AddFollower(int CustomerId, int VendorId)
        {
            Customer customer = db.Customers.Where(a=> a.Id == CustomerId).FirstOrDefault();
            Vendor vendor= db.Vendor.Where(a=> a.Id == VendorId).FirstOrDefault();
            Follow follower = new Follow();
            follower.CustomerId = CustomerId;
            follower.VendorId = VendorId;
            follower.isDeleted = false;
            db.follows.Add(follower);
            vendor.Followers.Add(customer);
            customer.Follow.Add(vendor);
            db.SaveChanges();
        }

        public List<Follow> GetAllFollowers(int VendorId)
        {
            var data = db.follows.Where(a =>  a.VendorId == VendorId && a.isDeleted == false).ToList();
            return data;
        }

        public void RemoveFollower(int CustomerId, int VendorId)
        {
            Follow follow = db.follows.Where(a => a.CustomerId == CustomerId && a.VendorId == VendorId).FirstOrDefault();
            follow.isDeleted = true;
            db.SaveChanges();

        }
    }
}
