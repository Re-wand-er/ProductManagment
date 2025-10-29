using Microsoft.AspNetCore.Mvc;
using ProductManagment.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using ProductManagment.Application.Interfaces;

namespace ProductManagment.WebUI.Controllers
{
    [Authorize(Roles = "AdvancedUser,Administrator")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Category() 
        { 
            return View(await GetCategoryAsync());
        }

        public async Task<IActionResult> Delete(int id) 
        {
            await _categoryService.Delete(id);
            return View("Category", await GetCategoryAsync());
        }

        private async Task<CategoryViewModel> GetCategoryAsync() 
        {
            var categoryEntity = await _categoryService.GetAll();
            var categories = categoryEntity
                .Select(c => new CategoryModel() { Id = c.Id, Name = c.Name })
                .ToList();

            return new CategoryViewModel() { categoryModels = categories }; 
        }

        public async Task Add(string category) 
        {
            await _categoryService.Add(category);
        }
    }
}
