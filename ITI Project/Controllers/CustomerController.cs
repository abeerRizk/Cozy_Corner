using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Project.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;

        public CustomerController(ICustomerService customerService , IMapper mapper)
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }

        public IActionResult Read()
        {
            var result = customerService.GetAll();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var customer = new CreateCustomerVM();
            return View(customer);
        }

        [HttpPost]
        public IActionResult Create(CreateCustomerVM customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var x = customerService.Create(customer);
                    return RedirectToAction("Read", "Customer");
                }
            }
            catch (Exception)
            {
                return View(customer);
            }

            return View(customer);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = customerService.GetByCustomerId(id);
            return View(data);
        }


        [HttpPost]
        public IActionResult Delete(GetCustomerVM customer)
        {
            try
            {
                customerService.Delete(customer.Id);
                return RedirectToAction("Read", "Customer");
            }
            catch (Exception)
            {
                return View(customer);
            }

        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = customerService.GetByCustomerId(id);
            UpdateCustomerVM new_data = mapper.Map<UpdateCustomerVM>(data);
            return View(new_data);
        }


        [HttpPost]
        public IActionResult Update(UpdateCustomerVM customer)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    customerService.Update(customer);
                    return RedirectToAction("Read", "Customer");
                }
            }
            catch (Exception)
            {
                return View(customer);
            }

            return View(customer);

        }
    }

}
