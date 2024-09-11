using ITI_Project.BLL.ModelVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Interface
{
    public interface IRatingService
    {
        public bool Create(CreateRatingVM rating);
        public bool Update(UpdateRatingVM rating);
        public bool Delete(int id);
        public IEnumerable<GetRatingVM> GetAll();

        public GetRatingVM GetByRatingId(int id);
    }
}
