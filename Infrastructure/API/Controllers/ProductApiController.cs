using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.DTOs;
using ProductManagment.Application.Interfaces;
using ProductManagment.Application.Services;

namespace ProductManagment.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductApiController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts() => Ok(await _productService.GetAll());

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetProduct(int id) => Ok(await _productService.GetValueById(id));


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return Ok(new { succes = true });
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO userModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); //_logger

            await _productService.AddAsync(userModel);
            return Ok(new { success = true });
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO userModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // _logger

            await _productService.Update(userModel);
            return Ok(new { success = true });
        }
    }
}
