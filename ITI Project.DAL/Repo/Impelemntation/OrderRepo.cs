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
            return db.Order.Include(o => o.Items).Where(a => a.IsDeleted != true).ToList();
        }

        public void AddOrder(Order order)
        {
            db.Order.Add(order);
            db.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                var data = db.Order.Where(a => a.Id == order.Id).FirstOrDefault();
                data.Items = order.Items;
                data.OrderDate = order.OrderDate;
                data.ShippingAddress = order.ShippingAddress;   
                data.CustomerId = order.CustomerId;
                data.CustomerName = order.CustomerName;
                data.Status = order.Status;
                data.ExpectedDeliveryDate = order.ExpectedDeliveryDate;
                data.IsDeleted = order.IsDeleted;
                data.TotalPrice = order.TotalPrice;
                
                data.TotalPrice = order.TotalPrice;
                
                db.SaveChanges();
               
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public void DeleteOrder(int id)
        {
            var order = db.Order.Where(a => a.Id == id).FirstOrDefault();
            order.IsDeleted = true;
            db.SaveChanges();

        }

        //public void AddOrderItem(int CustomerId, OrderItem item)
        //{

        //    Customer customer = db.Customers.Where(a=>a.Id == CustomerId).FirstOrDefault();

        //    if(customer.hasOrder == true)
        //    {
        //         var order = db.Order
        //        .Include(o => o.Items) 
        //        .FirstOrDefault(o => o.Id == customer.CurrentOrderId);


        //        /*  Check if it has order item with the same product */
        //        bool exist = db.OrderItems.Any(a => a.ProductId == item.ProductId && a.OrderId == item.OrderId);

        //        if(exist)
        //        {
        //            var orderItem =db.OrderItems.Where(a => a.ProductId == item.ProductId && a.OrderId == item.OrderId).FirstOrDefault();

        //            orderItem.Quantity += item.Quantity;
        //            orderItem.TotalPrice = orderItem.UnitPrice * orderItem.Quantity;
        //            order.TotalPrice += item.TotalPrice;

        //        }
        //        else
        //        {
        //            item.OrderId = customer.CurrentOrderId;
        //            order.TotalPrice += item.TotalPrice;
        //            order.Items.Add(item);
        //        }



        //        db.SaveChanges();
        //    }
        //    else
        //    {
        //        Order order = new Order();
        //        order.OrderDate = DateTime.Now;
        //        order.CustomerId = CustomerId;
        //        order.ShippingAddress = customer.Location;
        //        order.TotalPrice = item.TotalPrice;

        //        db.Order.Add(order); // Add the new order to the context
        //        db.SaveChanges(); // Save the new order so the database generates an OrderId

        //        customer.hasOrder = true;
        //        customer.CurrentOrderId = order.Id; // Now the Id will be set after the save

        //        item.OrderId = order.Id; // Now that order.Id is generated, assign it to the item

        //        if (order.Items == null) // Check if the Items collection is initialized
        //        {
        //            order.Items = new List<OrderItem>(); // Initialize if null
        //        }

        //        order.Items.Add(item); // Add the item to the order

        //        db.SaveChanges(); // Save the final changes, including the new OrderItem
        //    }


        //}


        public void AddOrderItem(int customerId, OrderItem item)
        {
            // Retrieve the customer
            Customer customer = db.Customers.FirstOrDefault(a => a.Id == customerId);

            if (customer != null)
            {
                if (customer.hasOrder == true)
                {
                    // Retrieve the current order and its items
                    var order = db.Order
                        .Include(o => o.Items)
                        .FirstOrDefault(o => o.Id == customer.CurrentOrderId);

                    if (order != null)
                    {
                        // Check if the order already contains the same product
                        var existingItem = order.Items.FirstOrDefault(a => a.ProductId == item.ProductId);

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
            Customer customer = db.Customers.FirstOrDefault(a => a.Id == customerId);

            if (customer != null)
            {
                var order = db.Order
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

    }
}
