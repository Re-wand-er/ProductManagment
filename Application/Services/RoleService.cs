using ProductManagment.Application.DTOs;
using ProductManagment.Application.Interfaces;
using ProductManagment.Domain.Interfaces;
using ProductManagment.WebUI.Models;

namespace ProductManagment.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleDTO>> GetAll() 
        {
            var categoriesEntity = await _roleRepository.GetAllAsync();
            return categoriesEntity.Select(r => new RoleDTO(r.Id, r.Name)).ToList();
        }
    }
}
