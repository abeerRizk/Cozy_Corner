using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Entites
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }
        public int? CutomerId { get; set; }
        public Customer? Customer { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public bool ? IsActive { get; set; }
    }
}
