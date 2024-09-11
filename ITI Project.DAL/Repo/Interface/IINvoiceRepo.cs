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
        public bool Create(Invoice invoice);
        public bool Update(Invoice invoice);
        public bool Delete(int id);
        public List<Invoice> GetAll();

        public Invoice GetByInvoiceId(int id);

    }
}
