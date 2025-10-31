using ProductManagment.Application.DTOs;
using ProductManagment.Application.Interfaces;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleService> _logger;
        public RoleService(IRoleRepository roleRepository, ILogger<RoleService> logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<RoleDTO>> GetAll() 
        {
            _logger.LogInformation("Получение всех ролей пользователя");
            var categoriesEntity = await _roleRepository.GetAllAsync();
            return categoriesEntity.Select(r => new RoleDTO(r.Id, r.Name)).ToList();
        }
    }
}
