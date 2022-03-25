using EnglishQuestionApp.Models.Identity;
using EnglishQuestionApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnglishQuestionApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Password = string.Empty;
                model.ConfirmPassword = string.Empty;
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if(user != null)
            {
                ModelState.AddModelError(nameof(model.UserName), "Bu kullanıcı adı daha önce sisteme kayıt edilmiştir");
                model.UserName = string.Empty;
                model.Password = string.Empty;
                model.ConfirmPassword = string.Empty;
                return View(model);
            }
            user = await _userManager.FindByEmailAsync(model.Email);
            if(user != null)
            {
                ModelState.AddModelError(nameof(model.Email), "Bu email daha önce sisteme kayıt edilmiştir");
                model.Email = string.Empty;
                model.Password = string.Empty;
                model.ConfirmPassword = string.Empty;
                return View(model);
            }
            user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kayıt işleminde bir hata oluştu");
                model.Password = string.Empty;
                model.ConfirmPassword = string.Empty;
                return View(model);
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
