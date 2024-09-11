using AutoMapper;
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
    public class FollowService : IFollowService
    {
        private readonly IFollowRepo followRepo;
        private readonly IMapper mapper;

        public FollowService(IFollowRepo followRepo , IMapper mapper)
        {
            this.followRepo = followRepo;
            this.mapper = mapper;
        }
        public bool AddFollower(int CustomerId, int VendorId)
        {
            try
            {
                followRepo.AddFollower(CustomerId, VendorId);
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public List<FollowVM> GetAllFollowers(int VendorId)
        {
            var data = followRepo.GetAllFollowers(VendorId);
            return mapper.Map < List <FollowVM> > (data);
        }

        public bool RemoveFollower(int CustomerId, int VendorId)
        {
            try
            {
                followRepo.RemoveFollower(CustomerId, VendorId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
