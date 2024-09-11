using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Entites
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public string? Status { get; set; }
        public int? Quantity { get; set; }
        public double? UnitPrice { get; set; }
        public double? TotalPrice { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int? VendorId { get; set; }
        public Vendor? Vendor { get; set; }


    }
}
