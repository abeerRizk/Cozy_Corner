using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Entites
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public int? Age { get; set; }
        public DateTime? Createdon { get; set; } = DateTime.Now;
        public bool? IsDeleted { get; set; }
        public string? Phone_Number { get; set; }
        public string? Location { get; set; }
        public List<Order>? Orders { get; set; }
        public List<Rating>? Ratings { get; set; }
        public List<Product>? FavProduct { get; set; }
        public List<Invoice>? Invoices { get; set; }

        public bool? hasOrder { get; set; }
        public int CurrentOrderId { get; set; }

        public List<Vendor>? Follow { get; set; }

    }
}
