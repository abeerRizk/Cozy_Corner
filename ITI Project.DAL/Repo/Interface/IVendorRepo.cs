using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Interface
{
    public interface IVendorRepo
    {
        Task <IEnumerable <Vendor>> GetAllVendors();
        Task<Vendor> GetVendorById(int id);
        Task<bool> AddVendor(Vendor vendor);
        Task<bool> UpdateVendor(Vendor vendor);
        Task<bool> DeleteVendor(int id);
        public Task< bool> IsEmailExist(string email);
        public Task<int> GetVendorId_ByUserId(string userId);
    }
}
