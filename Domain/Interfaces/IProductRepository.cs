using ProductManagment.Domain.Entities;

namespace ProductManagment.Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        IQueryable<Product> GetAllQuerable();
    }
}
