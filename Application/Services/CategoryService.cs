using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ProductManagment.Application.DTOs;
using ProductManagment.Application.Interfaces;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;
using System.ComponentModel;

namespace ProductManagment.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            this._categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll() 
        {
            _logger.LogInformation("Получение всех категорий");

            var categoryEntity = await _categoryRepository.GetAllAsync();
            var category = categoryEntity
                .Select(p => new CategoryDTO(p.Id, p.Name))
                .ToList();

            return category;
        }

        public async Task Add(string category) 
        {
            _logger.LogInformation($"Добавление категории: {category}");

            var categoryEntity = new Category()
            {
                Name = category
            };
            await _categoryRepository.AddAsync(categoryEntity);
            await _categoryRepository.SaveChangesAsync();
        }
        public async Task Delete(int id) 
        { 
            _logger.LogInformation($"Удаление категории с id: {id}");

            await _categoryRepository.DeleteAsync(id);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
