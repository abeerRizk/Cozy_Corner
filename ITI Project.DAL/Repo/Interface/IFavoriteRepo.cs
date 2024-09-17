
using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Interface
{
    public interface IFavoriteRepo
    {
        public  Task ChangeStatus(int customerId, int productId);

        public Task<bool> IsProductFavorite(int customerId, int productId);
    }
} 
