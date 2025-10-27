using Microsoft.EntityFrameworkCore;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbSet
                .Include(u => u.Role)
                .ToListAsync();
        }
    }
}
