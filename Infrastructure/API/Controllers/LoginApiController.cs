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
        public async Task<IActionResult> ValidateUser([FromBody] UserLoginPasswordDTO userDTO)
        {
            _logger.LogInformation($"Post /LoginApi/ValidateUser: Login={userDTO.Login}");

            var user = await _userService.GetValueByLogin(userDTO.Login);
            if (user == null)
                return BadRequest(new { message = "Неверный логин или пароль!" });

            bool isValid = await _userService.CheckPasswordAsync(userDTO.Login, userDTO.Password ?? "");
            if (!isValid)
                return BadRequest(new { message = "Неверный логин или пароль!" });

            if (user.IsBlocked)
                return BadRequest(new { message = "Пользователь заблокирован!" });

            return Ok(new
            {
                Id = user.Id,
                Login = user.Login,
                SystemRole = user.SystemRole
            });
        }
    }
}
