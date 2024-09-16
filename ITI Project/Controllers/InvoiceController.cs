using AutoMapper;

using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Project.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService invoiceService;
        private readonly IMapper mapper;

    

        public InvoiceController(IInvoiceService invoiceService, IMapper mapper )
        {
            this.invoiceService = invoiceService;
            this.mapper = mapper;
        }

        public IActionResult Read(int id )
        {
            var result =  invoiceService.GetByInvoiceId(id);
            return View(result);
        }


    }
}
