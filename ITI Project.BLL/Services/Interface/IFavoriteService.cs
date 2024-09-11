using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Interface
{
    public interface IFavoriteService
    {
        public  Task ChangeStatus(int customerId, int ProductId);
    }
}
