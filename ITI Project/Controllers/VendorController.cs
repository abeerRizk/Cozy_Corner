using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
namespace ITI_Project.Controllers
{
    public class VendorController : Controller
    {
        private readonly IVendorService _vendorService;
        private readonly IMapper _mapper;

        public VendorController(IVendorService vendorService, IMapper mapper)
        {
            _vendorService = vendorService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var vendors = _vendorService.GetAllVendors();

            if (vendors == null || !vendors.Any()) // Check if vendors are null or empty
            {
                return View(new List<GetAllVendorVM>()); // Pass an empty list to avoid null reference errors
            }

            return View(vendors);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateVendorVM vendorVM)
        {
            if (ModelState.IsValid)
            {
                var (success, message) = _vendorService.AddVendor(vendorVM);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, message);
                }
            }
            return View(vendorVM);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vendor = _vendorService.GetVendorById(id);
            if (vendor == null)
            {
                return NotFound();
            }

            var vendorVM = _mapper.Map<UpdateVendorVM>(vendor);
            return View(vendorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UpdateVendorVM vendorVM)
        {
            if (id != vendorVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var success = _vendorService.UpdateVendor(vendorVM);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update the vendor.");
                }
            }

            return View(vendorVM);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var vendor = _vendorService.GetVendorById(id);
            if (vendor == null)
            {
                return NotFound();
            }

            var vendorVM = _mapper.Map<GetAllVendorVM>(vendor);
            return View(vendorVM); 
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var success = _vendorService.DeleteVendor(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }



    }
}
