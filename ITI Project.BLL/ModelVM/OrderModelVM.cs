﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI_Project.DAL.Entites;

namespace ITI_Project.BLL.ModelVM
{
    public class OrderModelVM
    {
        [Key]
        public int Id { get; set; }
        public string? ShippingAddress { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string? PaymentMethod { get; set; }
        public double? TotalPrice { get; set; }
        public List<OrderItemsVM>? Items { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLocation { get; set; }

        public bool IsDeleted { get; set; }

        public int VendorId { get; set; }
        public string Phone_Number { get; set; }


    }
}
