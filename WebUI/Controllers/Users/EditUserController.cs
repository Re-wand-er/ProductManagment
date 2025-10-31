using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagment.WebUI.Contracts;
using ProductManagment.WebUI.Models;

namespace ProductManagment.WebUI.Controllers
{
    [Authorize]
    public class EditUserController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly ILogger<EditUserController> _logger;
        public EditUserController(ApiClient apiClient, ILogger<EditUserController> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IActionResult> Add(CreateUserModel userModel)
        {
            _logger.LogInformation($"Добавление пользователя (EditUser): Login={userModel.Login}, SystemRole={userModel.SystemRole}");
            await SendObjectToAction(userModel, "add");
            return RedirectToAction("Users", "User");
        }

        public async Task<IActionResult> Update(CreateUserModel userModel)
        {
            _logger.LogInformation($"Обновление пользователя (EditUser): Login={userModel.Login}, SystemRole={userModel.SystemRole}");
            await SendObjectToAction(userModel, "update");
            return RedirectToAction("Users", "User");
        }

        private async Task SendObjectToAction(CreateUserModel userModel, string action) 
        {
            var user = new
            {
                Id = userModel.Id,
                Login = userModel.Login,
                SystemRoleId = userModel.SystemRoleId,
                SystemRole = userModel.SystemRole,
                Email = userModel.Email,
                Password = userModel.Password
            };
            await _apiClient.SendObject($"api/UserApi/{action}", user);
        }
    }
}
