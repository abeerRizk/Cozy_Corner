using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ITI_Project.DAL.Entites
{
    [Table("User")]
    public class User : IdentityUser
    {
        public string? address { get; set; }
        public string? Image { get; set; }
        public string? FullName { get; set; }
        public bool? IsVendor {  get; set; }
        
    }
}
