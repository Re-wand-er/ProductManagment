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
        private readonly ILogger<LoginController> _logger;
        public LoginController(ApiClient apiClient, ILogger<LoginController> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Login() => View("Login");

        [HttpPost]
        public async Task<ActionResult> Login(LoginWithRoleModel loginModel) 
        {
            _logger.LogInformation($"Вход в аккаунт Post api/LoginApi: Login={loginModel.Login}");
            var user = await _apiClient.PostAndReadAsync<LoginWithRoleModel>("api/LoginApi", loginModel);

            if (!ModelState.IsValid || user == null)
            {
                ViewBag.Error = "Неверный логин или пароль!";
                return View("Login", loginModel);
            }

            if (user.IsBlocked)
            {
                ViewBag.Error = "Вы были заблокированы! Доступ к ресурсам запрещен!";
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
            _logger.LogInformation($"Выход из аккаунта");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", new LoginWithRoleModel());
        }
    }
}
