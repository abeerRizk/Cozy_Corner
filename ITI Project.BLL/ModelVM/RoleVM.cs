using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.ModelVM
{
    public class RoleVM
    {
        [Required]
        public string Rolename { get; set; }
    }
}
