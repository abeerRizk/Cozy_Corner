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
    public class OrderRepo : IOrderRepo
    {
        private readonly ApplicationDbContext db;

        public OrderRepo(ApplicationDbContext context)
        {
            db = context;
        }

        public Order GetOrderById(int id)
        {
            return db.Order.Include(o => o.Items).FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return db.Order.Include(o => o.Items).ToList();
        }

        public void AddOrder(Order order)
        {
            db.Order.Add(order);
            db.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            db.Order.Update(order);
            db.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            var order = db.Order.Find(id);
            if (order != null)
            {
                db.Order.Remove(order);
                db.SaveChanges();
            }
        }

        public void AddOrderItem(int id, Product item)
        {

            Order order = db.Order.Where(a => a.Id == id).FirstOrDefault();

            bool isExist = order.Items.Any(a => a.ProductId == item.Id);
            if (isExist)
            {
                OrderItem current = order.Items.Where(a => a.ProductId == item.Id).FirstOrDefault();
                current.Quantity += 1;
                current.TotalPrice = item.Price * current.Quantity;
                db.SaveChanges();
            }
            else
            {
                OrderItem new_item = new OrderItem();
                new_item.ProductId = item.Id;
                new_item.VendorId = item.VendorID;
                new_item.OrderId = id;
                new_item.UnitPrice = item.Price;
                new_item.TotalPrice = item.Price;
                new_item.Status = "ordered";
                order.Items.Add(new_item);
                db.SaveChanges();
            }
        }

        public void RemoveItem(int id, Product item)
        {
            Order order = db.Order.Where(a => a.Id == id).FirstOrDefault();
            bool isExist = order.Items.Any(a => a.ProductId == item.Id);
            if (isExist)
            {

                OrderItem current = order.Items.Where(a => a.ProductId == item.Id).FirstOrDefault();
                current.Quantity -= 1;
                current.TotalPrice = item.Price * current.Quantity;
                db.SaveChanges();
            }
        }

    }
}
