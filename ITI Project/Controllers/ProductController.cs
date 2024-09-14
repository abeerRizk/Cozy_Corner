﻿using AutoMapper;
using ITI_Project.BLL.Helper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Impelemntation;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace ITI_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IVendorService vendorService;
        private readonly UserManager<User> userManager;
        private readonly ICustomerService customerService;
        private readonly IFavoriteService favoriteService;

        public ProductController(IProductService productService, IMapper mapper , IVendorService vendorService ,
            UserManager<User> userManager , ICustomerService customerService , IFavoriteService favoriteService)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.vendorService = vendorService;
            this.userManager = userManager;
            this.customerService = customerService;
            this.favoriteService = favoriteService;
        }

        public async Task<IActionResult> Read()
        {
            var result = productService.GetAll();

            var user = await userManager.GetUserAsync(User);
            int customerId = customerService.GetCustomerId_ByUserId(user.Id);

            foreach (var product in result)
            {
                product.isFavorite = favoriteService.IsProductFavorite(customerId, product.Id);
            }

            return View(result);
        }


        public async Task<IActionResult> ViewProduct(int id)
        {
            var result = productService.GetByProductId(id);
            var user = await userManager.GetUserAsync(User);
            int customerId = customerService.GetCustomerId_ByUserId(user.Id);
            result.isFavorite = favoriteService.IsProductFavorite(customerId, id);
            return View(result);
        }



        [HttpGet]
        [Authorize(Roles = "Vendor")]
        public IActionResult Create()
        {
            var product = new CreateProductVM();
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Vendor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductVM model, IFormFileCollection images)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);

                var vendorId = vendorService.GetVendorId_ByUserId(user.Id);
                
                var product = new CreateProductVM
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Images = new List<string>(),
                    VendorID = vendorId
                };

                // Save multiple images
                foreach (var image in images)
                {
                    if (image != null && image.Length > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImgProduct/Profile", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        // Add the image URL to the product's image list
                        product.Images.Add(fileName);
                    }
                }
                productService.Create(product);
                // Save product to the database (using your repository or db context)
                // _context.Products.Add(product);
                // await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index)); // Or wherever you want to redirect
            }

            return View(model);
        }


      

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = productService.GetByProductId(id);
            return View(data);
        }


        [HttpPost]
        public IActionResult Delete(GetProductVM product)
        {
            try
            {
                productService.Delete(product.Id);
                return RedirectToAction("Delete", "Product");
            }
            catch (Exception)
            {
                return View(product);
            }

        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = productService.GetByProductId(id);
            UpdateProductVM new_data = mapper.Map<UpdateProductVM>(data);
            return View(new_data);
        }


        [HttpPost]
        public IActionResult Update(UpdateProductVM product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productService.Update(product);
                    return RedirectToAction("Update", "Product");
                }
            }
            catch (Exception)
            {
                return View(product);
            }

            return View(product);

        }
        public IActionResult Search(string searchTerm)
        {
            // Fetch all products first
            var allProducts = productService.GetAll();

            // Filter products based on the search term in both name and description
            var filteredProducts = allProducts
                .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                            p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Return the filtered product list to the view
            return View("Read", filteredProducts);
        }


    }
}
