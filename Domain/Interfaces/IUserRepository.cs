using ProductManagment.Domain.Entities;

namespace ProductManagment.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByLogin(string login);
        Task<bool> ExistByLoginAsync(string login);
        Task<bool> ExistByEmailAsync(string email);
    }
}
