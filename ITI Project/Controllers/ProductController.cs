using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Impelemntation;
using ITI_Project.BLL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITI_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        public IActionResult Read()
        {
            var result = productService.GetAll();
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
        public IActionResult Create(CreateProductVM product)
        {
            try
            {
                if (ModelState.IsValid)
                {
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

    }
}
