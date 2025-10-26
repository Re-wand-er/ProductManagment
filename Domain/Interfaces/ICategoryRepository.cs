using ProductManagment.Domain.Entities;
using ProductManagment.Infrastructure.Persistence.Repositories;

namespace ProductManagment.Domain.Interfaces
{
    interface ICategoryRepository : IBaseRepository<Category>
    {
    }
}
