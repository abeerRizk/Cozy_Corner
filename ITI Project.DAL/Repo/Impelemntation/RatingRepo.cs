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
    public class RatingRepo :IRatingRepo
    {
        private readonly ApplicationDbContext db;

        public RatingRepo(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        public bool Create(Rating rating)
        {
            try
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = db.Ratings.Where(a => a.Id == id).FirstOrDefault();
                if (data.IsDeleted == true)
                {
                    throw new Exception("The Rating is already deleted");

                }
                data.IsDeleted = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Rating> GetAll()
        {
            var result = db.Ratings.ToList();
            return result;
        }

        public Rating GetByRatingId(int id)
        {
            var data = db.Ratings.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }

        public bool Update(Rating rating)
        {
            try
            {
                var data = db.Ratings.Where(a => a.Id == rating.Id).FirstOrDefault();

                data.ReviewText = rating.ReviewText;
                data.RatingValue = rating.RatingValue;
                data.RatingDate = rating.RatingDate;
                data.Customer = rating.Customer;
               data.Product =   rating.Product;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
