using ProductManagment.Application.DTOs;

namespace ProductManagment.Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAll();
    }
}
