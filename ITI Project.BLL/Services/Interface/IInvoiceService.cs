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
        public int Create(CreateInvoiceVM invoice);

        public  Task<GetInvoiceVM>  GetByInvoiceId(int id);
        public  Task<GetInvoiceVM> GetInvoiceByOrderId(int orderId);
       
    }
}
