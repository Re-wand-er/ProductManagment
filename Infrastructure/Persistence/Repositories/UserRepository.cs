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

        public override async Task<User?> GetByIdAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User?> GetUserByLogin(string login) 
        {
            return await _dbSet
                .AsNoTracking()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(p => p.Login == login);
        }

        public async Task<bool> ExistByLoginAsync(string login) 
        {
            return await _dbSet
                .AnyAsync(u => u.Login.ToLower() == login.ToLower());    
        }
        public async Task<bool> ExistByEmailAsync(string email) 
        {
            return await _dbSet
                .AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
