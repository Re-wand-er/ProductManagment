using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Identity.Client;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class 
    {
        protected readonly DataBaseContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DataBaseContext dataBaseContext) 
        {
            _context = dataBaseContext;
            // _context.Set<T> возвращает коллекцию DbSet<T> из _context если она есть
            // если ее нет, то создает новый DbSet<T> с переданным T не являющийся Entity
            // при обращении в бд из такого _dbSet вызовет ошибку 
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() 
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity) 
        {
            await _dbSet.AddAsync(entity);
        }

        public void UpdateAsync(T entity) 
        {
            _dbSet.Update(entity);
        }

        public async Task SaveChangesAsync() 
        {
            await _context.SaveChangesAsync();
        }
    }
}
