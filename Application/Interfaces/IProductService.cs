using ProductManagment.Application.DTOs;

namespace ProductManagment.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAll();
        Task<ProductDTO?> GetValueById(int id);
        Task AddAsync(ProductDTO productDTO);
        Task Delete(int id);
        Task Update(ProductDTO productDTO);
    }
}
