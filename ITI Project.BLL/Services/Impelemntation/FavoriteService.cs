using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Impelemntation;
using ITI_Project.DAL.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Impelemntation
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepo favoriteRepo;

        public FavoriteService(IFavoriteRepo favoriteRepo)
        {
            this.favoriteRepo = favoriteRepo;
        }
        public async Task ChangeStatus(int customerId, int ProductId)
        {
            await favoriteRepo.ChangeStatus(customerId, ProductId);
        }
        //public async Task<bool> ToggleFavorite(string userId, int productId)
        //{
        //    // Call ChangeStatus method from repository
        //    await favoriteRepo.ChangeStatus(userId, productId);

        //    // Fetch the updated favorite item to determine its new status
        //    var favorite = favoriteRepo.GetFavorite(userId, productId);

        //    // Return the updated status
        //    return favorite != null && favorite.IsActive;
        //}

        public bool IsProductFavorite(int customerId, int productId)
        {
            return favoriteRepo.IsProductFavorite(customerId, productId);
        }

    }
}
