using ProductManagment.Domain.Interfaces;
using ProductManagment.Application.DTOs;
using ProductManagment.Domain.Entities;
using ProductManagment.Application.Interfaces;
using ProductManagment.WebUI.Models;

namespace ProductManagment.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger) 
        {
            _productRepository = productRepository; 
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDTO>> GetAll() 
        {
            _logger.LogInformation("Получение всех продуктов с категориями");

            var productEntities = await _productRepository.GetAllAsync();
            var product = productEntities
                .Select(p => new ProductDTO(p.Id, p.Name, p.CategoryId, p.Category.Name, p.Description, p.Cost, p?.GeneralNote ?? "", p?.SpecialNote ?? ""))
                .ToList();

            return product;
        }

        public async Task<ProductDTO?> GetValueById(int id) 
        {
            _logger.LogInformation($"Получение продукта с id: {id}");

            var entity = await _productRepository.GetByIdAsync(id);
            if (entity == null) { return null; }

            var product = new ProductDTO(entity.Id, entity.Name, entity.Category.Id, entity.Category.Name, entity.Description, entity.Cost, entity?.GeneralNote ?? "", entity?.SpecialNote ?? "");
            return product;
        }

        public async Task<IEnumerable<ProductDTO>> GetFilterAllAsync(string? search, int? categoryId, string? sort) 
        {
            _logger.LogInformation($"Фильтрация продуктов по поиску:{search}; Id категории: {categoryId}; сортировке: {sort}");
            var productEntities = await _productRepository.GetAllAsync();
            var product = productEntities
                .Select(p => new ProductDTO(p.Id, p.Name, p.CategoryId, p.Category.Name, p.Description, p.Cost, p?.GeneralNote ?? "", p?.SpecialNote ?? ""));

            if (!string.IsNullOrWhiteSpace(search))
                product = product.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

            if (categoryId.HasValue)
                product = product.Where(p => p.CategoryId == categoryId);

            product = sort switch
            {
                "name"              => product.OrderBy(p => p.Name),
                "name_desc"         => product.OrderByDescending(p => p.Name),
                "description"       => product.OrderBy(p => p.Description),
                "description_desc"  => product.OrderByDescending(p => p.Description),
                "cost"              => product.OrderBy(p => p.Cost),
                "cost_desc"         => product.OrderByDescending(p => p.Cost),
                "generalNote"       => product.OrderBy(p => p.GeneralNote),
                "generalNote_desc"  => product.OrderByDescending(p => p.GeneralNote),
                "specialNote"       => product.OrderBy(p => p.SpecialNote),
                "specialNote_desc"  => product.OrderByDescending(p => p.SpecialNote),
                _ => product
            };

            return product.ToList();
        }

        public async Task AddAsync(ProductDTO productDTO) 
        {
            _logger.LogInformation($"Добавление продукта: {productDTO.Name}; {productDTO.Cost}; {productDTO.Description}");

            var product = new Product()
            {
                Name = productDTO.Name,
                CategoryId = productDTO.CategoryId,
                Description = productDTO.Description,
                Cost = productDTO.Cost,
                GeneralNote = productDTO.GeneralNote,
                SpecialNote = productDTO.SpecialNote,
            };
            
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task Update(ProductDTO productDTO) 
        {
            _logger.LogInformation($"Обновление продукта: {productDTO.Name}; {productDTO.Cost}; {productDTO.Description}");

            var product = new Product()
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                CategoryId = productDTO.CategoryId,
                Description = productDTO.Description,
                Cost = productDTO.Cost,
                GeneralNote = productDTO.GeneralNote,
                SpecialNote = productDTO.SpecialNote
            };

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task Delete(int id) 
        {
            _logger.LogInformation($"Удаление продукта с id: {id}");

            await _productRepository.DeleteAsync(id);
            await _productRepository.SaveChangesAsync();
        }
    }
}
