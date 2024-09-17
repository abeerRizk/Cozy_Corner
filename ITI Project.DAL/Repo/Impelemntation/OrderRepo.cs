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

         public async Task<Order> GetOrderById(int id)
        {
            var data = await db.Order.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
            return data;
        }

        public async Task< IEnumerable<Order>> GetAllOrders()
        {
            return await db.Order.Include(o => o.Items).Where(a => a.IsDeleted != true).ToListAsync();
        }


        public void AddOrderItem(int customerId, OrderItem item)

        {
            // Retrieve the customer
            Customer customer =   db.Customers.FirstOrDefault(a => a.Id == customerId);

            if (customer != null)
            {
                if (customer.hasOrder == true)
                {
                    // Retrieve the current order and its items
                    var order =  db.Order
                        .Include(o => o.Items)
                        .FirstOrDefault(o => o.Id == customer.CurrentOrderId);

                    if (order != null)
                    {
                        // Check if the order already contains the same product
                        var existingItem =  order.Items.FirstOrDefault(a => a.ProductId == item.ProductId);

                        if (existingItem != null)
                        {
                            // Update the quantity and total price of the existing item
                            int? quantityDifference = item.Quantity;
                            existingItem.Quantity += item.Quantity;
                            existingItem.TotalPrice = existingItem.UnitPrice * existingItem.Quantity;

                            // Update the order's total price based on the difference in price
                            order.TotalPrice += existingItem.UnitPrice * quantityDifference;
                        }
                        else
                        {
                            // Add the new item to the existing order
                            item.OrderId = customer.CurrentOrderId;
                            order.TotalPrice += item.TotalPrice; // Update total price by adding new item's total price
                            order.Items.Add(item); // Add new item to the order
                        }

                         db.SaveChanges(); // Save all changes
                    }
                }
                else
                {
                    // Create a new order if the customer doesn't have one
                    Order order = new Order
                    {
                        OrderDate = DateTime.Now,
                        CustomerId = customerId,
                        ShippingAddress = customer.Location,
                        TotalPrice = item.TotalPrice, // Initialize with the first item's total price
                        Items = new List<OrderItem> { item }, // Add the item to the order
                        CustomerName = customer.Name,
                        Status = "ordered",
                    };

                     db.Order.Add(order);
                     db.SaveChanges(); // Save to generate the order's ID

                    // Update the customer's order tracking
                    customer.hasOrder = true;
                    customer.CurrentOrderId = order.Id;

                     db.SaveChanges(); // Save the changes to the customer
                }
            }
        }

        public void RemoveOrderItem(int customerId, OrderItem item)
        {
            Customer customer =  db.Customers.FirstOrDefault(a => a.Id == customerId);

            if (customer != null)
            {
                var order =   db.Order
                        .Include(o => o.Items)
                        .FirstOrDefault(o => o.Id == customer.CurrentOrderId);

            var existingItem = order.Items.FirstOrDefault(a => a.ProductId == item.ProductId);
                int? quantityDifference = item.Quantity;
                existingItem.Quantity -= item.Quantity;
                existingItem.TotalPrice = existingItem.UnitPrice * existingItem.Quantity;
     
                order.TotalPrice -= existingItem.UnitPrice * quantityDifference;
                if (existingItem.Quantity == 0)
                {
                    existingItem.IsDeleted = true;
                    order.Items.Remove(existingItem);
                }
                 db.SaveChanges();
            }
        }


        public  void UpdateOrder(Order order)
        {
            try
            {
                var data =  db.Order.Where(a => a.Id == order.Id).FirstOrDefault();
             
                data.OrderDate = order.OrderDate; data.ShippingAddress = order.ShippingAddress;
                data.CustomerId = order.CustomerId; data.CustomerName = order.CustomerName;
                data.Status = order.Status; data.ExpectedDeliveryDate = order.ExpectedDeliveryDate;
                data.IsDeleted = order.IsDeleted; data.TotalPrice = order.TotalPrice;
                data.TotalPrice = order.TotalPrice;
                data.Items = order.Items;
                 db.SaveChanges();
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public async Task UpdateOrderStatus(Order order)
        {
            try
            {
                var data = await db.Order.Where(a => a.Id == order.Id).FirstOrDefaultAsync();
                data.Status = order.Status; 

                await  db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public  void DeleteOrder(int id)
        {
            var order =  db.Order.Where(a=> a.Id == id).FirstOrDefault();
            order.IsDeleted = true;
            db.SaveChanges();
            
        }

        public void DeleteUnconfirmedOrders()
        {
            List<Order> order = db.Order.Where(a=>a.Status == "ordered" ).ToList();
            foreach (Order item in order)
            {
                Customer customer= db.Customers.Where(a=>a.Id == item.CustomerId).FirstOrDefault();
                customer.hasOrder = false;
                DeleteOrder(item.Id);
                db.SaveChanges();
            }
        }
        }
}
