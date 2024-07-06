using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using univercityProject.Services;
using univercityProject.Models.Dtos;
using univercityProject.Models.DBModel;

namespace univercityProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserservices _userServices;

        public AccountController(IUserservices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var valid = _userServices.ValidateUser(model.Username, model.Password);

                if (valid != 0 && ValidatePassword(model.Password))
                {
                    var user = _userServices.GetUserById(valid);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.ID.ToString()),
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.Role, user.Rule.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Login", "Account");
        }

        private bool ValidatePassword(string password)
        {
            // Replace this with your password validation logic
            return !string.IsNullOrEmpty(password);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
