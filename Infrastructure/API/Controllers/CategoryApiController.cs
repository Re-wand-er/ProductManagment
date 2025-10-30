using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.DTOs;
using ProductManagment.Application.Interfaces;
using ProductManagment.Application.Services;
using ProductManagment.WebUI.Models;

namespace ProductManagment.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryApiController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryApiController(ICategoryService categoryService) { _categoryService = categoryService; }
        [HttpGet] public async Task<IActionResult> GetCategories() => Ok(await _categoryService.GetAll());

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id) 
        {
            await _categoryService.Delete(id);
            return Ok(new { success = true });
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] string category) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); //_logger

            await _categoryService.Add(category);
            return Ok(new { success = true });
        }
    }
}
