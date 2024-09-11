using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Entites
{
    public class Follow
    {
        [Key]
        public int Id { get; set; }
        public int? VendorId { get; set; }
        public Vendor? Vendor { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public bool isDeleted {get; set;}
    }
}
