using ProductManagment.Domain.Interfaces;
using ProductManagment.Application.DTOs;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository; 
        public ProductService(IProductRepository productRepository) 
        { 
            _repository = productRepository; 
        }

        public async Task<IEnumerable<ProductDTO>> GetAll() 
        {
            var productEntities = await _repository.GetAllAsync();

            var product = productEntities
                .Select(p => new ProductDTO(p.Id, p.Name, p.CategoryId, p.Category.Name, p.Description, p.Cost, p?.GeneralNote ?? "", p?.SpecialNote ?? ""))
                .ToList();

            return product;
        }

        public async Task<ProductDTO?> GetValueById(int id) 
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) { return null; }

            var product = new ProductDTO(entity.Id, entity.Name, entity.Category.Id, entity.Category.Name, entity.Description, entity.Cost, entity?.GeneralNote ?? "", entity?.SpecialNote ?? "");
            return product;
        }

        public async Task AddAsync(ProductDTO productDTO) 
        {
            var product = new Product()
            {
                Name = productDTO.Name,
                CategoryId = productDTO.CategoryId,
                Description = productDTO.Description,
                Cost = productDTO.Cost,
                GeneralNote = productDTO.GeneralNote,
                SpecialNote = productDTO.SpecialNote,
            };

            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
        }

        public async Task Update(ProductDTO productDTO) 
        {
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

            _repository.UpdateAsync(product);
            await _repository.SaveChangesAsync();
        }

        public async Task Delete(int id) 
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
        }
    }
}
