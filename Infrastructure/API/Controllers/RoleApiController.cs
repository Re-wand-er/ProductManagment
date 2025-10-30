using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.Interfaces;

namespace ProductManagment.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleApiController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleApiController(IRoleService roleService) { _roleService = roleService; }
        [HttpGet] public async Task<IActionResult> GetRoles() => Ok(await _roleService.GetAll());
    }
}
