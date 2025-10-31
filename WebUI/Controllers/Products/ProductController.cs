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
            _logger.LogInformation($"Добавление продукта Get api/CategoryApi");
            var createProduct = new CreateProductModel() { Categories = await GetCategoriesAsync() };
            return View("EditProduct", createProduct);
        }

        public async Task<IActionResult> Edit(int id) 
        {
            _logger.LogInformation($"Получение продукта Get api/ProductApi/get/{id}");

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
            _logger.LogInformation("Получение всех продуктов и их отображение");
            var products = await GetProductsAsync();
            var categories = await GetCategoriesAsync();

            var productsModel = new ProductCatalogViewModel() { Products = products, Categories = categories };
            return View(productsModel);
        }

        [Authorize(Roles = "AdvancedUser,Administrator")]
        public async Task<IActionResult> Delete(int id) 
        {
            _logger.LogInformation($"Удаление продукта Delete api/ProductApi/delete/{id}");
            await _apiClient.DeleteObject($"api/ProductApi/delete/{id}");
            return RedirectToAction("Product", "Product");
        }

        public async Task<IActionResult> Filter(string? search, int? categoryId, string? sort) 
        {
            _logger.LogInformation($"Получение отфильтрованных продуктов по запросу: api/ProductApi/get?search={search}&categoryId={categoryId}&sort={sort}");
            var products = await _apiClient.GetFilterObjectAsync<ProductWithCategoryIdModel>(
                $"api/ProductApi/get?search={search}&categoryId={categoryId}&sort={sort}"
            ) ?? [];
            var categories = await GetCategoriesAsync();

            var productsModel = new ProductCatalogViewModel() { Products = products, Categories = categories };
            return View("Product", productsModel);
        }

        private async Task<IEnumerable<CategoryModel>> GetCategoriesAsync() =>  await _apiClient.GetObjectListAsync<CategoryModel>("api/CategoryApi") ?? [];
        private async Task<IEnumerable<ProductModel>> GetProductsAsync() => await _apiClient.GetObjectListAsync<ProductModel>("api/ProductApi") ?? [];
    }
}
