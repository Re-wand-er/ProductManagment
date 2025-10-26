using ProductManagment.Domain.Entities;

namespace ProductManagment.Domain.Interfaces
{
    interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        Task SaveChangesAsync();
    }
}
