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

namespace ITI_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IVendorService vendorService;
        private readonly UserManager<User> userManager;

        public ProductController(IProductService productService, IMapper mapper , IVendorService vendorService , UserManager<User> userManager)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.vendorService = vendorService;
            this.userManager = userManager;
        }

        public IActionResult Read()
        {
            var result = productService.GetAll();
            return View(result);
        }

        public IActionResult ViewProduct(int id)
        {
            var result = productService.GetByProductId(id);
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
        public async Task<IActionResult>  Create(CreateProductVM product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user =  await userManager.GetUserAsync(User);

                    var VendorId = vendorService.GetVendorId_ByUserId(user.Id);
                    product.VendorID = VendorId;
                    var x = productService.Create(product);
                    return RedirectToAction("Read", "Product");
                }
            }
            catch (Exception)
            {
                return View(product);
            }

            return View(product);
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
                return RedirectToAction("Read", "Product");
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
                    return RedirectToAction("Read", "Product");
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
