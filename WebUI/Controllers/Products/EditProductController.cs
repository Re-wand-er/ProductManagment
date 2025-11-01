using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.DTOs;
using ProductManagment.Application.Interfaces;
using ProductManagment.WebUI.Contracts;
using ProductManagment.WebUI.Models;

namespace ProductManagment.WebUI.Controllers
{
    [Authorize]
    public class EditProductController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly ILogger<EditProductController> _logger;
        public EditProductController(ApiClient apiClient, ILogger<EditProductController> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IActionResult> Add(CreateProductModel productModel)
        {
            _logger.LogInformation($"Создание продукта (EditProduct): Name={productModel.Name}, Description={productModel.Description} ,Category={productModel.Category}");

            var isValid = await ValidateAndSendObjectToAction(productModel, "add");
            if (!isValid)
                return View("EditProduct", productModel);

            return RedirectToAction("Product", "Product");
        }

        public async Task<IActionResult> Update(CreateProductModel productModel) 
        {
            _logger.LogInformation($"Обновление продукта (EditProduct): Name={productModel.Name}, Description={productModel.Description} ,Category={productModel.Category}");

            var isValid = await ValidateAndSendObjectToAction(productModel, "update");
            if (!isValid)
                return View("EditProduct", productModel);

            return RedirectToAction("Product", "Product");
        }

        private async Task<bool> ValidateAndSendObjectToAction(CreateProductModel productModel, string action)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("\n", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                ViewBag.ErrorProduct = errors;

                productModel.Categories = await _apiClient.GetObjectListAsync<CategoryModel>("api/CategoryApi") ?? [];
                return false;
            }

            try
            {
                var product = new
                {
                    Id = productModel.Id,
                    Name = productModel.Name,
                    CategoryId = productModel.CategoryId,
                    Category = productModel.Category,
                    Description = productModel.Description,
                    Cost = productModel.Cost,
                    GeneralNote = productModel.GeneralNote,
                    SpecialNote = productModel.SpecialNote
                };
                await _apiClient.SendObject($"api/ProductApi/{action}", product);
                return true;
            }
            catch (Exception ex)
            {
                productModel.Categories = await _apiClient.GetObjectListAsync<CategoryModel>("api/CategoryApi") ?? [];
                ViewBag.ErrorProduct = ex.Message;
                return false;
            }
        }
    }
}
