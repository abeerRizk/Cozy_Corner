using ITI_Project.BLL.ModelVM;
using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Interface
{
    public interface IFollowService
    {
        public bool AddFollower(int CustomerId, int VendorId);
        public bool RemoveFollower(int CustomerId, int VendorId);
        public List<FollowVM> GetAllFollowers(int VendorId);
    }
}
