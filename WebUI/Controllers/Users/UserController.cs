using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ProductManagment.Application.DTOs;
using ProductManagment.Application.Interfaces;
using ProductManagment.Application.Services;
using ProductManagment.WebUI.Contracts;
using ProductManagment.WebUI.Models;

namespace ProductManagment.WebUI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly ILogger<UserController> _logger;
        public UserController(ApiClient apiClient, ILogger<UserController> logger) 
        { 
            _apiClient = apiClient; 
            _logger = logger;
        }

        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation($"Получение пользователя Post api/UserApi/get/{id}");

            CreateUserModel createUser;
            var roles = await GetRolesAsync();
            var result = await _apiClient.GetObjectByIdAsync<UserWithPasswordModel>($"api/UserApi/get/{id}");

            if (result != null) createUser = new CreateUserModel(result, roles);
            else createUser = new CreateUserModel() {};

            return View("EditUser", createUser);
        }
        public async Task<IActionResult> Add()
        {
            _logger.LogInformation($"Добавление пользователя Get api/UserApi/add/");
            var createUser = new CreateUserModel() { Roles = await GetRolesAsync() };
            return View("EditUser", createUser);
        }

        public async Task<IActionResult> Users()
        {
            _logger.LogInformation($"Получение всех пользователя Get api/UserApi");
            var users = await _apiClient.GetObjectListAsync<UserModel>("api/UserApi");
            return View("User", users); 
        }

        public async Task<IActionResult> Block(int id) 
        {
            _logger.LogInformation($"Блокировка пользователя Post api/UserApi/block/{id}");
            await _apiClient.PatchObjectById($"api/UserApi/block/{id}");
            return RedirectToAction("Users", "User");
        }

        public async Task<IActionResult> Delete(int id) 
        {
            _logger.LogInformation($"Удаление пользователя Delete api/UserApi/delete/{id}");
            await _apiClient.DeleteObject($"api/UserApi/delete/{id}");
            return RedirectToAction("Users", "User");
        }

        private async Task<IEnumerable<RoleModel>> GetRolesAsync() => await _apiClient.GetObjectListAsync<RoleModel>("api/RoleApi") ?? [];
    }
}
