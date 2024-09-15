﻿using ITI_Project.BLL.ModelVM;
using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Interface
{
    public interface IOrderService
    {
        OrderModelVM GetOrderById(int id);
        IEnumerable<OrderModelVM> GetAllOrders();
        void AddOrder(OrderModelVM orderViewModel);
        void UpdateOrder(OrderModelVM orderViewModel);
        void DeleteOrder(int id);

        public void ConfirmOrder(int OrderId);


        public void AddOrderItem(int CustomerId, OrderItemsVM item);
        public void RemoveOrderItem(int CustomerId, OrderItemsVM item);
    }
}
