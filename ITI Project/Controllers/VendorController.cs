using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Impelemntation;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
namespace ITI_Project.Controllers
{
    public class VendorController : Controller
    {
        private readonly IVendorService _vendorService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> userManager;

        public VendorController(IVendorService vendorService, IMapper mapper
            , UserManager<User> userManager)
        {
            _vendorService = vendorService;
            _mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task< IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(User);
            int vendorId = _vendorService.GetVendorId_ByUserId(user.Id);
            var vendor = _vendorService.GetVendorById(vendorId);
            if (vendor == null)
            {
                return NotFound();
            }

            var vendorVM = _mapper.Map<UpdateVendorVM>(vendor);
            return View(vendorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Edit( UpdateVendorVM vendorVM)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                int vendorId = _vendorService.GetVendorId_ByUserId(user.Id);
                vendorVM.Id = vendorId;
                var success = _vendorService.UpdateVendor(vendorVM);
                if (success)
                {
                    return RedirectToAction("VendorGallery" , "Product");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update the vendor.");
                }
            }

            return View(vendorVM);
        }





    }
}
