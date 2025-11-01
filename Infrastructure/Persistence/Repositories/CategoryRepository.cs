using Microsoft.EntityFrameworkCore;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataBaseContext dbContext) : base(dbContext) 
        {}

        public async Task<bool> ExistByNameAsync(string category)
        {
            return await _dbSet.AnyAsync(c => c.Name.ToLower() == category.ToLower());
        }
    }
}
