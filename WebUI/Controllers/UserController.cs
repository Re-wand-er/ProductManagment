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
        public UserController(ApiClient apiClient) { _apiClient = apiClient; }

        public async Task<IActionResult> Edit(int id)
        {
            CreateUserModel createUser;

            var roles = await GetRolesAsync();
            var result = await _apiClient.GetObjectByIdAsync<UserWithPasswordModel>($"api/UserApi/get/{id}");

            if (result != null) createUser = new CreateUserModel(result, roles);
            else createUser = new CreateUserModel() {};

            return View("EditUser", createUser);
        }
        public async Task<IActionResult> Add()
        {
            var createUser = new CreateUserModel() { Roles = await GetRolesAsync() };
            return View("EditUser", createUser);
        }

        public async Task<IActionResult> Users()
        {
            var users = await _apiClient.GetObjectListAsync<UserModel>("api/UserApi");
            return View("User", users); 
        }

        public async Task<IActionResult> Block(int id) 
        {
            await _apiClient.PatchObjectById($"api/UserApi/block/{id}");
            return RedirectToAction("Users", "User");
        }

        public async Task<IActionResult> Delete(int id) 
        {
            await _apiClient.DeleteObject($"api/UserApi/delete/{id}");
            return RedirectToAction("Users", "User");
        }

        private async Task<IEnumerable<RoleModel>?> GetRolesAsync() => await _apiClient.GetObjectListAsync<RoleModel>("api/RoleApi");
    }
}
