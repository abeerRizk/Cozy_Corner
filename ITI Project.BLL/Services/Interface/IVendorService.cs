using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI_Project.BLL.ModelVM;

namespace ITI_Project.BLL.Services.Interface
{
    public interface IVendorService
    {
        Task< IEnumerable<GetAllVendorVM>> GetAllVendors();
        Task<Vendor> GetVendorById(int id);
        public Task<(bool sucess, string message)> AddVendor(CreateVendorVM vendor);
        public Task<bool> UpdateVendor(UpdateVendorVM vendorVM);
     
        public Task<int> GetVendorId_ByUserId(string userId);
    }
}
