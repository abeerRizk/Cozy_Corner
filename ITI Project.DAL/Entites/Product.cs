using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Entites
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? image {  get; set; }
        public string? Description { get; set; }
        public  bool? Available { get; set; }
        public int?  Quantity { get; set; }
        public double? Price { get; set; }
        public  string? Category { get; set; }
        public int? VendorID { get; set; }
        public Vendor? Vendor { get; set; }
        public List<Rating>? Ratings { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
