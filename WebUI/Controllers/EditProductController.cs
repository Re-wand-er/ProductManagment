using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.DTOs;
using ProductManagment.Domain.Interfaces;
using ProductManagment.WebUI.Models;

namespace ProductManagment.WebUI.Controllers
{
    public class EditProductController : Controller
    {
        private readonly IProductService _productService;
        public EditProductController(IProductService productService) 
        { 
            this._productService = productService; 
        }

        public async Task<IActionResult> Create(CreateProductModel productModel)
        {
            await _productService.AddAsync(GetProductDTO(productModel));
            return RedirectToAction("Product", "Product");
        }

        public IActionResult Update(CreateProductModel productModel) 
        {
            _productService.Update(GetProductDTO(productModel));
            return RedirectToAction("Product", "Product");
        }

        private ProductDTO GetProductDTO(CreateProductModel productModel) 
        { 
            return new ProductDTO(productModel.Id, productModel.Name, productModel.CategoryId, productModel.Category, productModel.Description ?? "", productModel.Cost, productModel.GeneralNote ?? "", productModel.SpecialNote ?? "");
        }
    }
}
