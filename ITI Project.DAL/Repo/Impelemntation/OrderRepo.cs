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
           return await db.Order.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task< IEnumerable<Order>> GetAllOrders()
        {
            return await db.Order.Include(o => o.Items).Where(a => a.IsDeleted != true).ToListAsync();
        }


        public async Task AddOrderItem(int customerId, OrderItem item)
        {
            // Retrieve the customer
            var customer = await  db.Customers.FirstOrDefaultAsync(a => a.Id == customerId);

            if (customer != null)
            {
                if (customer.hasOrder)
                {
                    // Retrieve the current order and its items
                    var order = await  db.Order.Include(o => o.Items)
                                              .FirstOrDefaultAsync(o => o.Id == customer.CurrentOrderId);

                    if (order != null)
                    {
                        // Check if the order already contains the same product
                        var existingItem =  db.OrderItems
                            .FirstOrDefault(a => a.ProductId == item.ProductId && a.OrderId == order.Id);

                        if (existingItem != null)
                        {
                            // Update the quantity and total price of the existing item
                            existingItem.Quantity += item.Quantity;
                            existingItem.TotalPrice = existingItem.UnitPrice * existingItem.Quantity;

                            // Update the order's total price
                            order.TotalPrice += item.UnitPrice * item.Quantity;
                        }
                        else
                        {
                            // Add the new item to the order
                            item.OrderId = order.Id;
                            order.TotalPrice += item.TotalPrice;
                            db.OrderItems.Add(item); // Directly add to OrderItems table
                        }

                       await  db.SaveChangesAsync(); // Save changes for both order and order items
                    }
                }
                else
                {
                    // Create a new order
                    Order order = new Order
                    {
                        OrderDate = DateTime.Now,
                        CustomerId = customerId,
                        ShippingAddress = customer.Location,
                        TotalPrice = item.TotalPrice,
                        Items = new List<OrderItem> { item },
                        CustomerName = customer.Name,
                        Status = "ordered",
                    };

                   await  db.Order.AddAsync(order);
                    await db.SaveChangesAsync();

                    // Update customer's order information
                    customer.hasOrder = true;
                    customer.CurrentOrderId = order.Id;

                     db.SaveChanges();
                }
            }
        }


        public async Task RemoveOrderItem(int customerId, OrderItem item)
        {
            var customer = await db.Customers.FirstOrDefaultAsync(a => a.Id == customerId);

            if (customer != null)
            {
                var order = await  db.Order
                        .Include(o => o.Items)
                        .FirstOrDefaultAsync(o => o.Id == customer.CurrentOrderId);

               
                var existingItem =  db.OrderItems.FirstOrDefault(a => a.ProductId == item.ProductId && a.OrderId == order.Id);
                int? quantityDifference = item.Quantity;
                existingItem.Quantity -= item.Quantity;
                existingItem.TotalPrice = existingItem.UnitPrice * existingItem.Quantity;
     
                order.TotalPrice -= existingItem.UnitPrice * quantityDifference;
                if (existingItem.Quantity == 0)
                {
                    existingItem.IsDeleted = true;
                    order.Items.Remove(existingItem);
                }
                  await db.SaveChangesAsync();
            }
        }


        public  async Task UpdateOrder(Order order)
        {
            try
            {
                var data = await  db.Order.Where(a => a.Id == order.Id).FirstOrDefaultAsync();
             
                data.OrderDate = order.OrderDate; data.ShippingAddress = order.ShippingAddress;
                data.CustomerId = order.CustomerId; data.CustomerName = order.CustomerName;
                data.Status = order.Status; data.ExpectedDeliveryDate = order.ExpectedDeliveryDate;
                data.IsDeleted = order.IsDeleted; data.TotalPrice = order.TotalPrice;
                data.TotalPrice = order.TotalPrice;
                data.Items = order.Items;
                 await db.SaveChangesAsync();
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
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public async Task DeleteOrder(int id)
        {
            var order = await  db.Order.Where(a=> a.Id == id).FirstOrDefaultAsync();
            order.IsDeleted = true;
            await db.SaveChangesAsync();
            
        }

        public async Task DeleteUnconfirmedOrders()
        {
            List<Order> order =  await db.Order.Where(a => a.Status == "ordered").ToListAsync();
            foreach (Order item in order)
            {
                var customer= await db.Customers.Where(a=>a.Id == item.CustomerId).FirstOrDefaultAsync();
                customer.hasOrder = false;
                // return Quantity to product
                 await  DeleteOrder(item.Id);
                 await db.SaveChangesAsync();
            }
        }
    }
}
