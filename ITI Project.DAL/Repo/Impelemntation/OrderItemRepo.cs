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




        public async Task< IEnumerable<OrderItem>> GetAll()
        {
            var data = await db.OrderItems.Where(a => a.IsDeleted != true).ToListAsync();
            return data;
        }

        public async Task< OrderItem> GetById(int id)
        {
            var item = await db.OrderItems.Where(a => a.Id == id).FirstOrDefaultAsync();
            return item;
        }


    }
}
