using ITI_Project.DAL.Entites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.ModelVM
{
    public class UpdateProductVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
     

        public string? Description { get; set; }
        public bool? Available { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public string? Category { get; set; }

        // A list of image URLs
        public List<string>? Images { get; set; }

        // Optional: you can add a property for file uploads
        public List<IFormFile>? ImageFiles { get; set; }
    }
}
