using System.Text.Json;
using ProductManagment.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProductManagment.Application.Interfaces;

namespace ProductManagment.WebUI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ICategoryService categoryService, ILogger<ProductController> logger)
        {
            this._productService = productService;
            this._categoryService = categoryService;
            _logger = logger;
        }

        //private readonly HttpClient _httpClient;
        //public CustomController(IHttpClientFactory factory)
        //{
        //    _httpClient = factory.CreateClient("ApiClient");
        //}

        public async Task<IActionResult> Create()
        {
            var createProduct = new CreateProductModel() { Categories = await GetCategoriesAsync() };

            return View("EditProduct", createProduct);
        }

        public async Task<IActionResult> Edit(int id) 
        {
            CreateProductModel product;

            var categories = await GetCategoriesAsync();
            var result = await _productService.GetValueById(id);

            if (result != null)
            { 
                product = new CreateProductModel() 
                { 
                    Id          = result.Id,
                    Name        = result.Name,
                    CategoryId  = result.CategoryId,
                    Categories  = categories,
                    Category    = result.Category,
                    Description = result.Description,
                    Cost        = result.Cost,
                    GeneralNote = result.GeneralNote,
                    SpecialNote = result.SpecialNote,
                };
            }
            else 
            {
                product = new CreateProductModel() { Categories = categories };
            }

            return View("", product);
        }

        [HttpGet]
        public async Task<IActionResult> Product()
        {
            _logger.LogInformation("Получение продуктов");
            //var productsDTO = await GetObjectListAsync<ProductDTO>("api/ProductApi");

            return View(await GetProductsAsync());
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "AdvancedUser,Administrator")]
        public async Task<IActionResult> Delete(int id) 
        {
            //var response = await _httpClient.DeleteAsync($"api/ProductApi/{id}");

            //var productsDTO = await GetObjectListAsync<ProductDTO>("api/ProductApi");
            await _productService.Delete(id);

            return View("Product", await GetProductsAsync());
        }

        //[HttpPost]
        //public async Task<IActionResult> Update([FromBody] ProductModel product)
        //{
        //    if (product != null)
        //    {
        //        var newProduct = new ProductDTO
        //        (
        //            product.Id,
        //            product.Name,
        //            product.Category,
        //            product.Description ?? "",
        //            product.Cost,
        //            product.GeneralNote ?? "",
        //            product.SpecialNote ?? ""
        //        );
        //        await _productService.Update(newProduct);
        //    }

        //    return View("Product");
        //}

        private async Task<List<CategoryModel>> GetCategoriesAsync() 
        {
            var categoriesEntity = await _categoryService.GetAll();
            return categoriesEntity.Select(c => new CategoryModel() { Id = c.Id, Name = c.Name }).ToList();
        }

        private async Task<IEnumerable<ProductModel>?> GetProductsAsync() 
        {
            var productsDTO = await _productService.GetAll();
            return productsDTO?
                .Select(p => new ProductModel(p.Id, p.Name, p.Category, p.Description, p.Cost, p.GeneralNote, p.SpecialNote))
                .ToList();
        }
        //private async Task<IEnumerable<T>?> GetObjectListAsync<T>(string apiController)
        //{
        //    var response = await _httpClient.GetAsync(apiController);
        //    var json = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<IEnumerable<T>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //}
    }
}
