using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Settimana_3_Manuel.Models;
using Settimana_3_Manuel.Service;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Settimana_3_Manuel.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            var u = _authService.Login(user.Name, user.Password);
            if (u == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, u.Name),
                new Claim(ClaimTypes.Email, u.Email),
                new Claim(ClaimTypes.NameIdentifier, u.Id.ToString())
            };
            u.Roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.Name)));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            _authService.Register(user);
            return RedirectToAction("Login");
        }
    }
}
