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

        public void AddOrderItem(int CustomerId, OrderItem item)
        {

            Customer customer = db.Customers.Where(a=>a.Id == CustomerId).FirstOrDefault();

            if(customer.hasOrder == true)
            {
                 var order = db.Order
                .Include(o => o.Items) 
                .FirstOrDefault(o => o.Id == customer.CurrentOrderId);

                item.OrderId = customer.CurrentOrderId;
                order.Items.Add(item);               
                db.SaveChanges();
            }
            else
            {
                Order order = new Order();
                order.OrderDate = DateTime.Now;
                order.CustomerId = CustomerId;
                order.ShippingAddress = customer.Location;

                db.Order.Add(order); // Add the new order to the context
                db.SaveChanges(); // Save the new order so the database generates an OrderId

                customer.hasOrder = true;
                customer.CurrentOrderId = order.Id; // Now the Id will be set after the save

                item.OrderId = order.Id; // Now that order.Id is generated, assign it to the item

                if (order.Items == null) // Check if the Items collection is initialized
                {
                    order.Items = new List<OrderItem>(); // Initialize if null
                }

                order.Items.Add(item); // Add the item to the order

                db.SaveChanges(); // Save the final changes, including the new OrderItem
            }


        }



    }
}
