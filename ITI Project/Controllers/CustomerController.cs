using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Implementation;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Project.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public CustomerController(ICustomerService customerService , IMapper mapper ,
            UserManager<User> userManager)
        {
            this.customerService = customerService;
            this.mapper = mapper;
            this.userManager = userManager;
        }



        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task< IActionResult> Update()
        {
            var user = await userManager.GetUserAsync(User);
            var customerId = await customerService.GetCustomerId_ByUserId(user.Id);
            var data =  await customerService.GetByCustomerId(customerId);
            UpdateCustomerVM new_data = mapper.Map<UpdateCustomerVM>(data);
           
            return View(new_data);
        }


        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Update(UpdateCustomerVM customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await customerService.Update(customer);
                    return RedirectToAction("Read", "Product");
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
