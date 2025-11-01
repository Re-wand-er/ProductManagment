using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagment.WebUI.Contracts;
using ProductManagment.WebUI.Models;
using System;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

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
            
            bool isValid = await ValidateAndSendObjectToAction(userModel, "add");
            if (!isValid) 
                return View("EditUser", userModel);
            
            return RedirectToAction("Users", "User"); 
        }

        public async Task<IActionResult> Update(CreateUserModel userModel)
        {
            _logger.LogInformation($"Обновление пользователя (EditUser): Login={userModel.Login}, SystemRole={userModel.SystemRole}");
            
            var isValid = await ValidateAndSendObjectToAction(userModel, "update");
            if (!isValid) 
                return View("EditUser", userModel);
            
            return RedirectToAction("Users", "User");
        }

        private async Task<bool> ValidateAndSendObjectToAction(CreateUserModel userModel, string action) 
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("\n", ModelState.Values
                    .SelectMany(e => e.Errors)
                    .Select(e => e.ErrorMessage));

                ViewBag.ErrorUser = errors;

                userModel.Roles = await _apiClient.GetObjectListAsync<RoleModel>("api/RoleApi") ?? [];
                return false;
            }

            var user = new
            {
                Id = userModel.Id,
                Login = userModel.Login,
                SystemRoleId = userModel.SystemRoleId,
                SystemRole = userModel.SystemRole,
                Email = userModel.Email,
                Password = userModel.Password
            };
            var response = await _apiClient.SendObject($"api/UserApi/{action}", user);

            if (!string.IsNullOrWhiteSpace(response))
            {
                ViewBag.ErrorUser = response;
                userModel.Roles = await _apiClient.GetObjectListAsync<RoleModel>("api/RoleApi") ?? [];
                return false;
            }

            return true;
        }
    }
}
