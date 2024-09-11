using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Entites
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public  string? ShippingAddress { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string? Status { get; set; }
        public DateTime  ExpectedDeliveryDate { get; set; }
        public string? PaymentMethod { get; set; }
        public double? TotalPrice { get; set; }
        public List<OrderItem>? Items { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
