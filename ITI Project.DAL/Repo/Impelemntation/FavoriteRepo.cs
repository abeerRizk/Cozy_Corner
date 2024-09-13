using ITI_Project.DAL.DB.ApplicationDB;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using Microsoft.EntityFrameworkCore;
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
        public Favorite GetFavorite(int userId, int productId)
        {
            return db.Favorites
                .FirstOrDefault(f => f.CutomerId == userId && f.ProductId == productId);
        }

        public void AddFavorite(Favorite favorite)
        {
            db.Favorites.Add(favorite);
            db.SaveChanges();
        }

        public void RemoveFavorite(Favorite favorite)
        {
           db.Favorites.Remove(favorite);
            db.SaveChanges();
        }

    }


}
