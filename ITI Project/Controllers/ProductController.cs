using AutoMapper;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private readonly INotificationService notificationService;

        public ProductController(IProductService productService, IMapper mapper , IVendorService vendorService ,
            UserManager<User> userManager , ICustomerService customerService , IFavoriteService favoriteService
            , INotificationService notificationService)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.vendorService = vendorService;
            this.userManager = userManager;
            this.customerService = customerService;
            this.favoriteService = favoriteService;
            this.notificationService = notificationService;
        }

        public async Task<IActionResult> Read()
        {
            var result = await productService.GetAll();
            if ( User.IsInRole("Customer"))
            {
                var user = await userManager.GetUserAsync(User);
                int customerId = await customerService.GetCustomerId_ByUserId(user.Id);

                foreach (var product in result)
                {
                    product.isFavorite = await favoriteService.IsProductFavorite(customerId, product.Id);
                }
            }

            return View(result);
        }

        public async Task <IActionResult> VendorGallery()
        {
            var user = await userManager.GetUserAsync(User);
            int vendorId = await vendorService.GetVendorId_ByUserId(user.Id);
            var result = await productService.GetAll() ;
            var new_result = result.Where(a => a.VendorId == vendorId && a.IsDeleted != true);

            return View(new_result);
        }


        public async Task<IActionResult> ViewProduct(int id)
        {
            var result = await productService.GetByProductId(id);
            var user = await userManager.GetUserAsync(User);
            int customerId = await customerService.GetCustomerId_ByUserId(user.Id);
            result.isFavorite =  await favoriteService.IsProductFavorite(customerId, id);
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

                var vendorId =  await vendorService.GetVendorId_ByUserId(user.Id);
                if (vendorId == null)
                {
                    // Handle the case where the vendor does not exist for the user
                    ModelState.AddModelError("", "Vendor not found for the current user.");
                    return View(model);
                }

                var product = new CreateProductVM
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Images = new List<string>(),
                    VendorID = vendorId,
                    Quantity = model.Quantity,
                    Available = model.Available,
                    Category = model.Category,
                    
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
                await productService.Create(product);

                await notificationService.SendNotificationToGoogleEmails();
                return RedirectToAction("VendorGallery", "Product"); // Or wherever you want to redirect
            }

            return View(model);
        }


      

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await productService.GetByProductId(id);
            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(GetProductVM product)
        {
            try
            {
                // delete Existing images
                var data = await productService.GetByProductId(product.Id);
                foreach (var oldImage in data.Images)
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImgProduct/Profile", oldImage);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                data.Images.Clear();

                await productService.Delete(product.Id);
                return RedirectToAction("VendorGallery", "Product");
            }
            catch (Exception)
            {
                return View(product);
            }

        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var data = await productService.GetByProductId(id);
            UpdateProductVM new_data = mapper.Map<UpdateProductVM>(data);
            return View(new_data);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductVM product, IFormFileCollection images)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    // delete Existing images
                    var data = await productService.GetByProductId(product.Id);
                    foreach (var oldImage in data.Images)
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImgProduct/Profile", oldImage);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    data.Images.Clear();
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
                    await productService.Update(product);
                    return RedirectToAction("VendorGallery", "Product");
                }
            }
            catch (Exception)
            {
                return View(product);
            }

            return View(product);

        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            // Fetch all products first
            var allProducts = await productService.GetAll();

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
