using HartCheck_Admin.Data;
using HartCheck_Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HartCheck_Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Admin> _userManager;
        private readonly SignInManager<Admin> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<Admin> userManager, SignInManager<Admin> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress); 

            if(user != null)
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    //Password is correct
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "EducationalResource");
                    }
                    
                }
                //Password is incorrect
                TempData["Error"] = "Invalid Credentials. Please try again";
                return View(loginViewModel);
            }
            //User not found
            TempData["Error"] = "Invalid Credentials. Please try again";
            return View(loginViewModel);
        }
        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use.";
                return View(registerViewModel);
            }

            var newAdmin = new Admin()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };
            var newAdminResponse = await _userManager.CreateAsync(newAdmin, registerViewModel.Password);

            if (newAdminResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newAdmin, UserRoles.Admin);
            }
            return RedirectToAction("Index", "EducationalResource");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "EducationalResource");
        }
    }
}

