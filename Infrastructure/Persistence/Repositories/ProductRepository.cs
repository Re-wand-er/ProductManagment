using Microsoft.EntityFrameworkCore;
using ProductManagment.Application.DTOs;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }

        public override async Task<IEnumerable<Product>> GetAllAsync() 
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Category)
                .ToListAsync();
        }

        public override async Task<Product?> GetByIdAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public IQueryable<Product> GetAllQuerable()
        {
            IQueryable<Product> products = _dbSet;

            return products
                .AsNoTracking()
                .Include(p => p.Category);
        }
    }
}
