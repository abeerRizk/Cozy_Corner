using ITI_Project.BLL.ModelVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Interface
{
    public interface ICustomerService
    {
        public Task<(bool sucess, string message)> Create(CreateCustomerVM customer);
        public Task<bool> Update(UpdateCustomerVM product);
 
        public Task<IEnumerable<GetCustomerVM>> GetAll();
        public Task<GetCustomerVM> GetByCustomerId(int id);

        public Task<int> GetCustomerId_ByUserId(string userId);
    }
}
