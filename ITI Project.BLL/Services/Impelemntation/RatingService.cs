using AutoMapper;
using ITI_Project.BLL.Helper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Impelemntation
{
    public class RatingService :IRatingService
    {

        private readonly IRatingRepo ratingRepo;
        private readonly IMapper mapper;

        public RatingService(IRatingRepo ratingRepo, IMapper mapper)
        {
            this.ratingRepo = ratingRepo;
            this.mapper = mapper;
        }

        public bool Create(CreateRatingVM rating)
        {
            try
            {

                Rating new_rating = mapper.Map<Rating>(rating);

                ratingRepo.Create(new_rating);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {

                ratingRepo.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<GetRatingVM> GetAll()
        {
            var result = ratingRepo.GetAll().Where(a => a.IsDeleted == false).ToList();
            List<GetRatingVM> newResult = mapper.Map<List<GetRatingVM>>(result);
            return newResult;
        }

        public GetRatingVM GetByRatingId(int id)
        {
            Rating rating = ratingRepo.GetByRatingId(id);
            return mapper.Map<GetRatingVM>(rating);
        }

        public bool Update(UpdateRatingVM rating)
        {
            try
            {
                Rating new_rating = mapper.Map<Rating>(rating);

                ratingRepo.Update(new_rating);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

    }
}
