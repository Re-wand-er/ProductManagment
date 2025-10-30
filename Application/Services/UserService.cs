using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagment.Application.DTOs;
using ProductManagment.Application.Interfaces;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            _logger.LogInformation("Получение всех пользователей");

            var users = await _userRepository.GetAllAsync();
            var user = users
                .Select(u => new UserDTO(u.Id, u.Login, u.Role.Name, u.Email, u.IsBlocked))
                .ToList();

            return user;
        }

        public async Task<UserWithPasswordDTO?> GetValueById(int id)
        {
            _logger.LogInformation($"Получение пользователя по id: {id}");

            var entity = await _userRepository.GetByIdAsync(id);
            if (entity == null) { return null; }

            var user = new UserWithPasswordDTO(entity.Id, entity.Login, entity.Role.Id, entity.Role.Name, entity.Email, "");
            return user;
        }

        public async Task<UserDTO?> GetValueByLogin(string login) 
        {
            _logger.LogInformation($"Получение пользователя по логину: {login}");

            var entity = await _userRepository.GetUserByLogin(login);
            // ВАЛИДАЦИЯ
            if (entity == null) { return null; }

            var user = new UserDTO(entity.Id, entity.Login, entity.Role.Name, entity.Email, entity.IsBlocked);
            return user;
        }

        public async Task Add(UserWithPasswordDTO userDTO) 
        {
            _logger.LogInformation($"Добавление пользователя: {userDTO.Login}; {userDTO.Email}");

            var user = new User()
            {
                Login = userDTO.Login,
                RoleId = userDTO.SystemRoleId,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password
            };
            // Проверка на валидацию/ошибки и т.д.
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdatePasswordAsync(int id, string newPassword) 
        {
            _logger.LogInformation($"Изменение пароля для пользователя с id: {id}");

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return;

            user.PasswordHash = newPassword; // HashPassword() хэширование пароля
            _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task Delete(int id) 
        {
            _logger.LogInformation($"Удаление пользователя под id: {id}");

            var entity = _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();
        }


        public async Task Block(int id) 
        {
            _logger.LogInformation($"Блокировка пользователя под id: {id}");

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
