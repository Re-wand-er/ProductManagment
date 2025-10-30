using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.Interfaces;
using ProductManagment.Application.DTOs;

namespace ProductManagment.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginApiController : ControllerBase
    {
        private readonly IUserService _userService;
        public LoginApiController(IUserService userService) { _userService = userService; }

        [HttpPost]
        public async Task<IActionResult> ValidateUser(UserLoginPasswordDTO userDTO) 
        {
            var user = await _userService.GetValueByLogin(userDTO.Login); // можно попробовать передавать UserLoginPasswordDTO

            if (userDTO.Password == null || user == null)
                return BadRequest(new { message = "Неверный логин или пароль!" });

            return Ok(new
            {
                Id = user.Id,
                Login = user.Login,
                SystemRole = user.SystemRole
            });
        }
    }
}
