using ProductManagment.Domain.Entities;

namespace ProductManagment.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
