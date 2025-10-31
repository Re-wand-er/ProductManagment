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
        private readonly ILogger<LoginApiController> _logger;
        public LoginApiController(IUserService userService, ILogger<LoginApiController> logger) 
        { 
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> ValidateUser(UserLoginPasswordDTO userDTO) 
        {
            var user = await _userService.GetValueByLogin(userDTO.Login); // можно попробовать передавать UserLoginPasswordDTO

            _logger.LogInformation($"Post /LoginApi/ValidateUser: Login={userDTO.Login}");

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
