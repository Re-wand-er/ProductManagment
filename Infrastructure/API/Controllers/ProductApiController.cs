using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.DTOs;
using ProductManagment.Application.Interfaces;
using ProductManagment.Application.Services;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductApiController> _logger;
        public ProductApiController(IProductService productService, ILogger<ProductApiController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts() => Ok(await _productService.GetAll());

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetProduct(int id) => Ok(await _productService.GetValueById(id));

        [HttpGet("get/")]
        public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] int? categoryId, [FromQuery] string? sort)
        {
            _logger.LogInformation($"Get /ProductApi/get/ GetAll: search={search}, categoryId={categoryId}, sort={sort}");
            var products = await _productService.GetFilterAllAsync(search, categoryId, sort);
            return Ok(products);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Delete /ProductApi/delete/{id} Delete");
            await _productService.Delete(id);
            return Ok(new { succes = true });
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO productModel)
        {
            _logger.LogInformation($"Post /ProductApi/add/ AddProduct: Login={productModel.Name}, Category={productModel.Category}");
            if (!ModelState.IsValid) return BadRequest(ModelState); 

            await _productService.AddAsync(productModel);
            return Ok(new { success = true });
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productModel)
        {
            _logger.LogInformation($"Post /ProductApi/update/ UpdateProduct: Login={productModel.Name}, Category={productModel.Category}\"");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _productService.Update(productModel);
            return Ok(new { success = true });
        }
    }
}
