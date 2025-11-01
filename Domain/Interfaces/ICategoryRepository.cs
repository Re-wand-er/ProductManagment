using ProductManagment.Domain.Entities;
using ProductManagment.Infrastructure.Persistence.Repositories;

namespace ProductManagment.Domain.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<bool> ExistByNameAsync(string category);
    }
}
