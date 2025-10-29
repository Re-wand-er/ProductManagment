using ProductManagment.Application.DTOs;

namespace ProductManagment.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAll();
        Task Delete(int id);

        Task Add(string category);
    }
}
