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

        public async Task<IActionResult> Create(CreateProductModel productModel)
        {
            _logger.LogInformation($"Создание продукта (EditProduct): Name={productModel.Name}, Description={productModel.Description} ,Category={productModel.Category}");
            await SendObjectToAction(productModel, "add");
            return RedirectToAction("Product", "Product");
        }

        public async Task<IActionResult> Update(CreateProductModel productModel) 
        {
            _logger.LogInformation($"Обновление продукта (EditProduct): Name={productModel.Name}, Description={productModel.Description} ,Category={productModel.Category}");
            await SendObjectToAction(productModel, "update");
            return RedirectToAction("Product", "Product");
        }

        private async Task SendObjectToAction(CreateProductModel productModel, string action)
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
        }
    }
}
