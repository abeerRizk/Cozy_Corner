using ITI_Project.BLL.ModelVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Interface
{
    public interface IInvoiceService
    {
        public bool Create(CreateInvoiceVM invoice);
        public bool Update(UpdateInvoiceVM invoice);
        public bool Delete(int id);
        public IEnumerable<GetInvoiceVM> GetAll();

        public GetInvoiceVM GetByInvoiceId(int id);
    }
}
