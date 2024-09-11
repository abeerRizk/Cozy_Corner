using ITI_Project.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ITI_Project.BLL.ModelVM;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ITI_Project.BLL.Services.Implementation;
using ITI_Project.BLL.Services.Interface;
namespace ITI_Project.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IVendorService vendorService;
        private readonly ICustomerService customerService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager , IVendorService vendorService , ICustomerService customerService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.vendorService = vendorService;
            this.customerService = customerService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
  
        public async Task<IActionResult> Register(RegisterUserVM newUserVM)
        {
            if (ModelState.IsValid)
            {
                User userModel = new User
                {
                    UserName = newUserVM.UserName,
                    Email = newUserVM.Email,
                    address = newUserVM.Address
                };

                // Attempt to create the user
                IdentityResult result = await userManager.CreateAsync(userModel, newUserVM.Password);

                if (result.Succeeded)
                {
                    // Determine if the user is a Vendor or Client
                    if (newUserVM.IsVendor.HasValue && newUserVM.IsVendor.Value)
                    {
                        CreateVendorVM vendor = new CreateVendorVM
                        {
                            Name = newUserVM.UserName,
                            Email = newUserVM.Email
                        };

                        vendorService.AddVendor(vendor);
                        await userManager.AddToRoleAsync(userModel, "Vendor");
                    }
                    else
                    {
                        CreateCustomerVM customer = new CreateCustomerVM
                        {
                            Name = newUserVM.UserName,
                            Email = newUserVM.Email
                        };

                        customerService.Create(customer);
                        await userManager.AddToRoleAsync(userModel, "Customer");

                    }

                    // Sign in the user after registration
                    await signInManager.SignInAsync(userModel, false);

                    // Redirect to the home page or another page upon successful registration
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // If user creation failed, add errors to the model state
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If ModelState is invalid or errors occurred, return the view with the validation errors
            return View(newUserVM);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserVM userVM)
        {
            if (ModelState.IsValid)
            {
                //create cookie 
                User userModel = await userManager.FindByNameAsync(userVM.UserName);
                if (userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, userVM.Password);
                    if (found == true)
                    {
                        await signInManager.SignInAsync(userModel, userVM.RemenberMe);
                        return RedirectToAction("Index", "Home");

                    }
                }
                ModelState.AddModelError("", "user name or password are wrong ");


            }
            return View(userVM);
        }
        public IActionResult LogOut()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        


    }
}
