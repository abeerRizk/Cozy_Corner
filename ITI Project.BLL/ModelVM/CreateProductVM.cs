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
    public class CreateProductVM
    {


        [Required]
        public string Name { get; set; }
        public IFormFile? ImageName { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public bool? Available { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public string? Category { get; set; }
        public int? VendorID { get; set; }
        public Vendor? Vendor { get; set; }

    }
}
