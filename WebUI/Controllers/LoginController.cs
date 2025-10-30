using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.Interfaces;
using ProductManagment.WebUI.Contracts;
using ProductManagment.WebUI.Models;
using System.Security.Claims;

namespace ProductManagment.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApiClient _apiClient;
        public LoginController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet]
        public ActionResult Login() => View("Login");

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginModel) 
        {
            var user = await _apiClient.PostAndReadAsync<LoginWithRoleModel>("api/LoginApi", loginModel);

            // нужно сравнивать user.Password != loginModel.Password
            if (user == null) // нужна валидация
            {
                ViewBag.Error = "Неверный логин или пароль!";
                return View("Login", loginModel);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.SystemRole ?? "")
            };

            var authentication = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync
            (
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(authentication)
            );

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Logout() 
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", new LoginModel());
        }
    }
}
