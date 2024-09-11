using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.ModelVM
{
    public class UpdateRatingVM
    {
        [Key]
        public int Id { get; set; }
        public int? RatingValue { get; set; }
        public string? ReviewText { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
