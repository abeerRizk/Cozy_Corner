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

        public int Create(CreateInvoiceVM invoice)
        {

            var data = mapper.Map<Invoice>(invoice);
            int InvoiceId =  iNvoiceRepo.Create(data);
            return InvoiceId;

        }



  

        public async Task< GetInvoiceVM> GetByInvoiceId(int id)
        {
            Invoice invoice = await iNvoiceRepo.GetByInvoiceId(id);
            return mapper.Map<GetInvoiceVM>(invoice);
        }

        public async Task<GetInvoiceVM> GetInvoiceByOrderId(int orderId)
        {
            Invoice invoice = await iNvoiceRepo.GetInvoiceByOrderId(orderId);
            return mapper.Map<GetInvoiceVM>(invoice);
        }


    }
}
