using ITI_Project.DAL.DB.ApplicationDB;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
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

        public void Create(OrderItem item)
        {
            db.OrderItems.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = db.OrderItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                {
                    db.Remove(item);
                    db.SaveChanges();

                }
            }
        }

        public IEnumerable<OrderItem> GetAll()
        {
            var data = db.OrderItems.ToList();
            return data;
        }

        public OrderItem GetById(int id)
        {
            var item = db.OrderItems.Where(a => a.Id == id).FirstOrDefault();
            return item;
        }

        public bool Update(OrderItem item)
        {
            try
            {
                var data = db.OrderItems.Where(a => a.Id == item.Id).FirstOrDefault();
                data.Quantity = item.Quantity;
                data.Status=item.Status;
                data.Quantity=item.Quantity;
                data.TotalPrice = item.TotalPrice;
                data.UnitPrice = item.UnitPrice;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex) 
                {
                return false;
                }
        }
    }
}
