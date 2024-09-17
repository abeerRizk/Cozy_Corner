using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Impelemntation;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ITI_Project.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFavoriteService _favoriteService;
        private readonly IUserService _userService; // Assuming you have a service to get the logged-in user
        private readonly UserManager<User> _userManager;
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;

        public FavoritesController(IProductService productService, IFavoriteService favoriteService, IUserService userService, UserManager<User> userManager 
            , ICustomerService customerService, IMapper mapper)
        {
            _productService = productService;
            _favoriteService = favoriteService;
            _userService = userService;
            _userManager = userManager;
            this.customerService = customerService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ToggleFavorite(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(); 
            }

            var customerId = await customerService.GetCustomerId_ByUserId(user.Id);
            await _favoriteService.ChangeStatus(customerId, productId);

            // Get the referring URL from the request header
            string refererUrl = Request.Headers["Referer"].ToString();

            // If the referrer URL is present, redirect back to it, otherwise redirect to a fallback action
            if (!string.IsNullOrEmpty(refererUrl))
            {
                return Redirect(refererUrl);  // Redirect to the referring view
            }

            return RedirectToAction("Read" , "Product");  //
            
        }

        public async Task<IActionResult> FavoriteProducts()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var customerId = await customerService.GetCustomerId_ByUserId(user.Id);
            var favoriteProducts = await _productService.GetFavoriteProductsByCustomerId(customerId);

            // Map the Product entities to GetProductVM
            var favoriteProductVMs = favoriteProducts.Select(p => new GetProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Available = p.Available,
                Quantity = p.Quantity,
                Price = p.Price,
                Category = p.Category,
                Images = p.Images, // Ensure that images are correctly retrieved
                isFavorite = true, // Set isFavorite to true since these are favorite products
                 IsDeleted =  p.IsDeleted      
            }).ToList().Where(a=> a.IsDeleted != true);

            return View(favoriteProductVMs);
        }



    }
}

