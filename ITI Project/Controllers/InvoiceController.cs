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

        public InvoiceController(IInvoiceService invoiceService, IMapper mapper)
        {
            this.invoiceService = invoiceService;
            this.mapper = mapper;
        }

        public IActionResult Read()
        {
            var result =  invoiceService.GetAll();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var invoice = new CreateInvoiceVM();
            return View(invoice);
        }

        [HttpPost]
        public IActionResult Create(CreateInvoiceVM invoice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var x = invoiceService.Create(invoice);
                    return RedirectToAction("Read", "Invoice");
                }
            }
            catch (Exception)
            {
                return View(invoice);
            }

            return View(invoice);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = invoiceService.GetByInvoiceId(id);
            return View(data);
        }


        [HttpPost]
        public IActionResult Delete(GetInvoiceVM invoice)
        {
            try
            {
                invoiceService.Delete(invoice.Id);
                return RedirectToAction("Read", "Invoice");
            }
            catch (Exception)
            {
                return View(invoice);
            }

        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = invoiceService.GetByInvoiceId(id);
            UpdateInvoiceVM new_data = mapper.Map<UpdateInvoiceVM>(data);
            return View(new_data);
        }


        [HttpPost]
        public IActionResult Update(UpdateInvoiceVM invoice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    invoiceService.Update(invoice);
                    return RedirectToAction("Read", "Invoice");
                }
            }
            catch (Exception)
            {
                return View(invoice);
            }

            return View(invoice);

        }
    }
}
