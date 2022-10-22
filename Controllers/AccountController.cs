using AspNetCoreExample.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AspNetCoreExample.Models;

namespace AspNetCoreExample.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager; // oturum yönetimi
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AccountController(
            SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName,Password")] UserVM model)
        {
            // önce kullanıcıyı username göre bul. Email adresi bizde UserName'dir
            AppUser user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
                user = await _userManager.FindByEmailAsync(model.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

                if (!result.Succeeded)
                {
                    ViewBag.Error = "Hatalı şifre";
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Error = "KUllanıcı bulunamadı";
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> CheckUserName(string UserName)
        {
            AppUser user = await _userManager.FindByNameAsync(UserName);

            if (user != null)
                return Json($"{UserName} başka bir kullanıcı tarafından alınmış");
            else
                return Json("");
        }

        public async Task<IActionResult> CheckEmailAdress(string EmailAdres)
        {
            AppUser user = await _userManager.FindByEmailAsync(EmailAdres);

            if (user != null)
                return Json($"{EmailAdres} başka bir kullanıcı tarafından alınmış");
            else
                return Json("");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserVM model)
        {
            if (ModelState.IsValid)
            {

                AppUser user = new AppUser();
                user.UserName = model.UserName;
                user.Name = model.Name;
                user.SurName = model.SurName;
                user.Email = model.Email;

                var result = await _userManager.CreateAsync(user, model.Password); // kullanıcıyı oluşturur
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                        ModelState.AddModelError(item.Code, item.Description);
                    return View();
                }

                // kullanıcıları üye rolü ile sisteme kayıt edelim...
                if (!await _roleManager.RoleExistsAsync("uye")) // role yok ise
                {
                    AppRole role = new AppRole();
                    role.Name = "uye";
                    await _roleManager.CreateAsync(role);
                }

                if (!await _roleManager.RoleExistsAsync("admin"))
                {
                    AppRole role = new AppRole();
                    role.Name = "admin";
                    await _roleManager.CreateAsync(role);
                }

                await _userManager.AddToRoleAsync(user, "uye"); // user'a üye rolünü set et..
                if (user.Email == "ekrem.yildirim@windowslive.com")
                {
                    await _userManager.AddToRoleAsync(user, "admin");
                }

                await _signInManager.PasswordSignInAsync(user, model.Password, true, false); // oturum aç
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync(); // oturumu kapat cookileri sill
            return RedirectToAction("Index", "Home");
        }
    }
}
