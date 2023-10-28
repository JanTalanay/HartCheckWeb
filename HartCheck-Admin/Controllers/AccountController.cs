using HartCheck_Admin.Data;
using HartCheck_Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HartCheck_Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            LoginViewModel vm = new LoginViewModel();
            if (!string.IsNullOrEmpty(returnUrl))
                vm.ReturnUrl = returnUrl;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string? returnUrl)
        {
            IdentityUser user = await _userManager.FindByNameAsync(loginViewModel.EmailAddress);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                        return LocalRedirect(returnUrl);
                    else
                        return LocalRedirect("/Home/Index");          
                }
                else
                {
                    ModelState.AddModelError("Login Error", "Invalid Credentials");
                    return View(loginViewModel);
                }
            }
            else
            {
                ModelState.AddModelError("Login Error", "Invalid Credentials");
                return View(loginViewModel);
            }
        }
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid == true)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = registerViewModel.EmailAddress;
                user.Email = registerViewModel.EmailAddress;
                await _userManager.CreateAsync(user, registerViewModel.Password);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(registerViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

