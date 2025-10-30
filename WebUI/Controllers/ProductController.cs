using System.Text.Json;
using ProductManagment.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProductManagment.Application.Interfaces;
using ProductManagment.WebUI.Contracts;

namespace ProductManagment.WebUI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, ApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Add()
        {
            var createProduct = new CreateProductModel() { Categories = await GetCategoriesAsync() };
            return View("EditProduct", createProduct);
        }

        public async Task<IActionResult> Edit(int id) 
        {
            CreateProductModel product;

            var categories = await GetCategoriesAsync();
            var result = await _apiClient.GetObjectByIdAsync<ProductWithCategoryIdModel>($"api/ProductApi/get/{id}");

            if (result != null) product = new CreateProductModel(result, categories);
            else product = new CreateProductModel() { };

            return View("EditProduct", product);
        }

        [HttpGet]
        public async Task<IActionResult> Product()
        {
            _logger.LogInformation("Получение продуктов");
            var products = await _apiClient.GetObjectListAsync<ProductModel>("api/ProductApi");
            return View(products);
        }

        [Authorize(Roles = "AdvancedUser,Administrator")]
        public async Task<IActionResult> Delete(int id) 
        {
            await _apiClient.DeleteObject($"api/ProductApi/delete/{id}");
            return RedirectToAction("Product", "Product");
        }

        private async Task<IEnumerable<CategoryModel>?> GetCategoriesAsync() =>  await _apiClient.GetObjectListAsync<CategoryModel>("api/CategoryApi");
    }
}
