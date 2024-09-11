using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Interface
{
    public interface IRatingRepo
    {
        public bool Create(Rating rating);
        public bool Update(Rating rating);
        public bool Delete(int id);
        public List<Rating> GetAll();

        public Rating GetByRatingId(int id);
    }
}
