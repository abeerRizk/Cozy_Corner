using ITI_Project.DAL.DB.ApplicationDB;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Impelemntation
{
    public class OrderItemRepo : IOrderItemRepo
    {
        private readonly ApplicationDbContext db;

        public OrderItemRepo(ApplicationDbContext db)
        {
            this.db = db;
        }




        public async Task< IEnumerable<OrderItem>> GetAll(int OrderId , int vendorId)
        {
            var data = await db.OrderItems.Where(a => a.IsDeleted != true && a.OrderId== OrderId && a.VendorId == vendorId).ToListAsync();
            return data;
        }

        public async Task< OrderItem> GetById(int id)
        {
            var item = await db.OrderItems.Where(a => a.Id == id).FirstOrDefaultAsync();
            return item;
        }
        public async Task Delete(int itemId)
        {
            var item = await db.OrderItems.Where(a => a.Id == id).FirstOrDefaultAsync();
            item.IsDeleted = true;
            await db.SaveChangesAsync();
        }


    }
}
