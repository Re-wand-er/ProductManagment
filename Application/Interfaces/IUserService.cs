using ProductManagment.Application.DTOs;
using System.ComponentModel;

namespace ProductManagment.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserWithPasswordDTO?> GetValueById(int id);
        Task<UserDTO?> GetValueByLogin(string login);
        Task<UserDTO> ValidateUser(UserLoginPasswordDTO userDTO);
        Task<bool> CheckPasswordAsync(string login, string password);
        Task Add(UserWithPasswordDTO userDTO);
        Task Block(int id);
        Task UpdatePasswordAsync(int id, string Password);
        Task Delete(int id);
    }
}
