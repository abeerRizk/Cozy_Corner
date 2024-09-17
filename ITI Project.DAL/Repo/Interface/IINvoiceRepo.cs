using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Interface
{
    public interface IINvoiceRepo
    {
        public int Create(Invoice invoice);


        public Task<Invoice> GetByInvoiceId(int id);
        public Task<Invoice> GetInvoiceByOrderId(int orderId);

      


    }
}
