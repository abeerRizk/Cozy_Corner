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

        public FavoritesController(IProductService productService, IFavoriteService favoriteService, IUserService userService, UserManager<User> userManager)
        {
            _productService = productService;
            _favoriteService = favoriteService;
            _userService = userService;
            _userManager = userManager;
        }

        //[HttpPost]
        //public async Task<IActionResult> ToggleFavorite(int productId)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return Unauthorized(); // If the user is not authenticated, return an Unauthorized result
        //    }

        //    var userId = user.Id;
        //    //var isFavorite = await _favoriteService.ToggleFavorite(userId, productId);

        //    return Json(new { isFavorite });
        //}
    }
}
