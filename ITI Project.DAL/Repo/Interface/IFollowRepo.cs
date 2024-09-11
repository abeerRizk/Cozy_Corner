using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Interface
{
    public interface IFollowRepo
    {
        public void AddFollower(int CustomerId, int VendorId);
        public void RemoveFollower(int CustomerId, int VendorId);
        public  List<Follow> GetAllFollowers(int VendorId);
    }
}
