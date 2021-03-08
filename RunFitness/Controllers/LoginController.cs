using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunFitness.DAL;
using RunFitness.Extensions;
using RunFitness.Models;
using RunFitness.ViewModels;

namespace RunFitness.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;
        public LoginController(AppDbContext db, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
           
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
        }
        public IActionResult Index()
        {
           
            return View();
        }
        #region Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginVM login)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByEmailAsync(email: login.EmailAddress);
            if (user == null)
            {
                ModelState.AddModelError("", "Email ve ya password sehfdi!!!");
                return View();
            }

            if (user.IsDeleted)
            {
                ModelState.AddModelError("", "Hesab bloklanib!!!");
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager
                .PasswordSignInAsync(user, login.Password, true, true);

            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Bir nece deqiqe gozleyin!!!");
                return View(login);
            }

            if (signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email ve ya password sehfdi!!!");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion


        public IActionResult Register()
        {
            return View();
        }


        #region Register

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();
            AppUser newUser = new AppUser
            {
                Fullname = register.Fullname,
                Email = register.Email,
                UserName = register.Username

            };
            IdentityResult identityResult = await _userManager.CreateAsync(newUser, register.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            //await _userManager.AddToRoleAsync(newUser, Roles.Admin.ToString());
            await _signInManager.SignInAsync(newUser, true);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Signup()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }

        #endregion
    }
}
