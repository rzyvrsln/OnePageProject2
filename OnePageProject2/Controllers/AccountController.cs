using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnePageProject2.Models;
using OnePageProject2.ViewModels.AppUser;

namespace OnePageProject2.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {

            if (!ModelState.IsValid) return View();

            AppUser appUser = new AppUser
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                UserName = registerVM.UserName,
                Email = registerVM.Email
            };

            IdentityResult result = await _userManager.CreateAsync(appUser,registerVM.Password);

            if (!result.Succeeded)
            {

                foreach (var error in result.Errors) { ModelState.AddModelError("", error.Description); return View(); }
            }

            //await _userManager.AddToRoleAsync(appUser, "Admin");
            return RedirectToAction(nameof(Login),"Account");
        }

        [HttpGet]
        public async Task<IActionResult> Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View();
            var user = await _userManager.FindByNameAsync(loginVM.UserName);
            if (user is null) { ModelState.AddModelError("Username", "Bele bir istifadeci yoxdur."); return View(); }

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.IsParsistance,true);
            if(!result.Succeeded) { ModelState.AddModelError("Username", "Bele bir istifadeci yoxdur."); return View(); }

            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index),"Home");
        }

        //[HttpGet]
        //public async Task<IActionResult> AddRoles()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    return View();
        //}
    }
}
