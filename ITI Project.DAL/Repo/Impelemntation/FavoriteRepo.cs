using ITI_Project.DAL.DB.ApplicationDB;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Impelemntation
{
    public class FavoriteRepo : IFavoriteRepo
    {
        private readonly ApplicationDbContext db;

        public FavoriteRepo(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task ChangeStatus(int customerId, int productId)
        {

            var existingFavorite = db.Favorites
                .FirstOrDefault(f => f.CutomerId == customerId && f.ProductId == productId);

            if (existingFavorite == null)
            {
                var favorite = new Favorite
                {
                    CutomerId = customerId,
                    ProductId = productId,
                    IsActive = true
                };

                db.Favorites.Add(favorite);


            }
            else
            {
                existingFavorite.IsActive = !existingFavorite.IsActive;
            }
            await db.SaveChangesAsync();

        }

    }


}
