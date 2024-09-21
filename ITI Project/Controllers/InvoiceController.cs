using AutoMapper;

using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Project.Controllers
{
    [Authorize(Roles = "Customer")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService invoiceService;
        private readonly IMapper mapper;

    

        public  InvoiceController(IInvoiceService invoiceService, IMapper mapper )
        {
            this.invoiceService = invoiceService;
            this.mapper = mapper;
        }

       
        public async Task<IActionResult> Read(int id )
        {
            var result =  await invoiceService.GetByInvoiceId(id);
            return View(result);
        }


    }
}
