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

        public CategoryController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Category()
        {
            var category = await _apiClient.GetObjectListAsync<CategoryModel>("api/CategoryApi");
            var model = new CategoryViewModel() { categoryModels = category};
            return View(model);
        }

        public async Task<IActionResult> Delete(int id) 
        {
            await _apiClient.DeleteObject($"api/CategoryApi/delete/{id}");
            return RedirectToAction("Category", "Category");
        }

        public async Task<IActionResult> Add(CategoryViewModel model) 
        {
            if (string.IsNullOrWhiteSpace(model.Category)) return View("Category", model);

            await _apiClient.SendObject("api/CategoryApi/add", model.Category);
            return RedirectToAction("Category", "Category");
        }
    }
}
