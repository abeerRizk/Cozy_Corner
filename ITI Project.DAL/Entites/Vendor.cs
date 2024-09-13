﻿using Azure.Core.Pipeline;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Entites
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public  string  Email { get; set; }
        public int? Age { get; set; }
        public DateTime? Createdon { get; set; } = DateTime.Now;
        public bool? IsDeleted { get; set; }
        public string? Phone_Number { get; set; }
        public string? Location { get; set; } 
        public List<Product>? Products { get; set; }
        public List<Customer>? Followers { get; set; }
        public string userId { get; set; }
    }

}
