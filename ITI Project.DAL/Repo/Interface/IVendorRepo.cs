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
        IEnumerable<Vendor> GetAllVendors();
        Vendor GetVendorById(int id);
        bool AddVendor(Vendor vendor);
        bool UpdateVendor(Vendor vendor);
        bool DeleteVendor(int id);
        public bool IsEmailExist(string email);
    }
}
