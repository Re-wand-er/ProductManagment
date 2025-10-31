using Microsoft.AspNetCore.Mvc;
using ProductManagment.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using ProductManagment.Application.Interfaces;
using ProductManagment.WebUI.Contracts;

namespace ProductManagment.WebUI.Controllers
{
    [Authorize(Roles = "AdvancedUser,Administrator")]
    public class CategoryController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ApiClient apiClient, ILogger<CategoryController> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IActionResult> Category()
        {
            _logger.LogInformation($"Получение категорий обращение к api: Get api/CategoryApi");
            var category = await _apiClient.GetObjectListAsync<CategoryModel>("api/CategoryApi") ?? [];
            var model = new CategoryViewModel() { categoryModels = category };
            return View(model);
        }

        public async Task<IActionResult> Delete(int id) 
        {
            _logger.LogInformation($"Удаление категории к api: Delete api/CategoryApi/delete/{id}");
            await _apiClient.DeleteObject($"api/CategoryApi/delete/{id}");
            return RedirectToAction("Category", "Category");
        }

        public async Task<IActionResult> Add(CategoryViewModel model) 
        {
            _logger.LogInformation($"Добавление категории к api: Post api/CategoryApi/add");
            if (string.IsNullOrWhiteSpace(model.Category)) return View("Category", model);

            await _apiClient.SendObject("api/CategoryApi/add", model.Category);
            return RedirectToAction("Category", "Category");
        }
    }
}
