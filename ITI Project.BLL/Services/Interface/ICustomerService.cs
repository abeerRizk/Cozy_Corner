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
        public (bool sucess, string message) Create(CreateCustomerVM customer);
        public bool Update(UpdateCustomerVM product);
        public bool Delete(int id);
        public IEnumerable<GetCustomerVM> GetAll();

        public GetCustomerVM GetByCustomerId(int id);
    }
}
