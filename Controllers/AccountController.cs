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
        public async Task<IActionResult> Login(UserVM model)
        {
            // önce kullanıcıyı username göre bul. Email adresi bizde UserName'dir
            AppUser user = await _userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
               var result =  await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

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

        [HttpPost]
        public async Task<IActionResult> Register(UserVM model)
        {
            if (ModelState.IsValid)
            {

                AppUser user = new AppUser();
                user.UserName = model.Email;
                user.Name = model.Name;
                user.SurName = model.SurName;
                user.Email = model.Email;

                var reuslt = await _userManager.CreateAsync(user, model.Password); // kullanıcıyı oluşturur


                // kullanıcıları üye rolü ile sisteme kayıt edelim...
                if (await _roleManager.RoleExistsAsync("uye")) // role yok ise
                {
                    AppRole role = new AppRole();
                    role.Name = "uye";
                    await _roleManager.CreateAsync(role);
                }


                await _userManager.AddToRoleAsync(user, "uye"); // user'a üye rolünü set et..


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
