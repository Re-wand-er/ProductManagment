using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.Services;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.WebUI.API.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductApiController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
           return Ok(await _productService.GetAll());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            await _productService.Delete(id);
            return Ok(new { succes = true });
        }
    }
}
