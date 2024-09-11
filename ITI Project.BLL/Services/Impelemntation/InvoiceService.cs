using AutoMapper;
using ITI_Project.BLL.Helper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Impelemntation
{
    public class InvoiceService :IInvoiceService
    {
        private readonly IINvoiceRepo iNvoiceRepo;
        private readonly IMapper mapper;

        public InvoiceService(IINvoiceRepo iNvoiceRepo, IMapper mapper)
        {
            this.iNvoiceRepo = iNvoiceRepo;
            this.mapper = mapper;
        }

        public bool Create(CreateInvoiceVM invoice)
        {
            try
            {
                var data = mapper.Map<Invoice>(invoice);
                iNvoiceRepo.Create(data);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {

                iNvoiceRepo.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<GetInvoiceVM> GetAll()
        {
            var result = iNvoiceRepo.GetAll().Where(a => a.isDeleted == false).ToList();
            List<GetInvoiceVM> newResult = mapper.Map<List<GetInvoiceVM>>(result);
            return newResult;
        }

        public GetInvoiceVM GetByInvoiceId(int id)
        {
            Invoice invoice = iNvoiceRepo.GetByInvoiceId(id);
            return mapper.Map<GetInvoiceVM>(invoice);
        }

        public bool Update(UpdateInvoiceVM invoice)
        {
            try
            {
                Invoice new_invoice = mapper.Map<Invoice>(invoice);
                
                iNvoiceRepo.Update(new_invoice);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

    }
}
