using Doctorantura.App.Context;
using Doctorantura.App.Extensions;
using Doctorantura.App.Models;
using Doctorantura.App.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Doctorantura.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _env;

        public AccountController(AppDbContext dbContext, UserManager<AppUser> userManager, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _env = env;
        }
        public IActionResult Register()
        {
            RegisterVM registerVm = new RegisterVM
            {
                Genders = _dbContext.Genders.ToList()
            };
            return View(registerVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            registerVM.Genders = await _dbContext.Genders.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            if (registerVM.Photo != null)
            {
                if (!registerVM.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Fayl şəkil tipində olmalıdır!!!");
                    return View(registerVM);
                }
            }

            string image = await registerVM.Photo.SaveAsync(_env.WebRootPath, "images", "profilePhotos");

            AppUser appUser = new AppUser
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                FatherName = registerVM.FatherName,
                Image = image,
                Email = registerVM.Email,
                UserName = registerVM.Email,
                GenderId = registerVM.GenderId
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            await _userManager.AddToRoleAsync(appUser, "User");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {

            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email ya da Parol səhvdir!!");
                return View(loginVM);
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.Remember, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email ya da Parol səhvdir!!");
                return View(loginVM);
            }

            var tt = User.Identity.IsAuthenticated;

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
