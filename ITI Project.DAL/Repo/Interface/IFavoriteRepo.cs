using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Interface
{
    public interface IFavoriteRepo
    {
        public Task ChangeStatus(int customerId, int productId);
    }
} 
