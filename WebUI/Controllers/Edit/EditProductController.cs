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
        public EditProductController(ApiClient apiClient) { _apiClient = apiClient; }

        public async Task<IActionResult> Create(CreateProductModel productModel)
        {
            await SendObjectToAction(productModel, "add");
            return RedirectToAction("Product", "Product");
        }

        public async Task<IActionResult> Update(CreateProductModel productModel) 
        {
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
