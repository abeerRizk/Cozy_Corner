using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.ModelVM
{
    public class AddNotificationVM
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string? Message { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsRead { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
