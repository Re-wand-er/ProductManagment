using ProductManagment.Application.DTOs;

namespace ProductManagment.Domain.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAll();

        Task Delete(int id);

        Task Add(UserWithPasswordDTO userDTO);

        Task Block(int id);
    }
}
