using ProductManagment.Application.DTOs;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();

            var user = users
                .Select(u => new UserDTO(u.Id, u.Login, u.Role.Name, u.Email, u.IsBlocked))
                .ToList();

            return user;
        }

        public async Task Add(UserWithPasswordDTO userDTO) 
        {
            var user = new User()
            {
                Login = userDTO.Login,
                RoleId = userDTO.SystemRoleId,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task Delete(int id) 
        {
            var entity = _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();
        }

        public async Task Block(int id) 
        {
            var entity = await _userRepository.GetByIdAsync(id);
            if (entity != null) 
            { 
                entity.IsBlocked = !entity.IsBlocked; 
                _userRepository.UpdateAsync(entity);
                await _userRepository.SaveChangesAsync();
            }
        }
    }
}
