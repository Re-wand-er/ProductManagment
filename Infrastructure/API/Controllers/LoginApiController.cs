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
            _logger.LogInformation("Post /LoginApi/ValidateUser: {@userDTO.Login}", userDTO);

            try
            {
                var user = await _userService.ValidateUser(userDTO);

                return Ok(new
                {
                    Id = user.Id,
                    Login = user.Login,
                    SystemRole = user.SystemRole
                });
            }
            catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
            catch (Exception ex) 
            {
                _logger.LogError($"Post /LoginApi/ValidateUser: Login={userDTO.Login}: {ex.Message}");
                return BadRequest();
            }
        }
    }
}
