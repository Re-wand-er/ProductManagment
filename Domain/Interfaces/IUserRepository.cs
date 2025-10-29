using ProductManagment.Domain.Entities;

namespace ProductManagment.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByLogin(string login);
    }
}
