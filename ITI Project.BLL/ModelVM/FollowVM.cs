using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.ModelVM
{
    public class FollowVM
    {
        public int? VendorId { get; set; }
        public Vendor? Vendor { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public bool isDeleted { get; set; }
    }
}
