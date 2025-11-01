using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.DTOs;
using ProductManagment.Application.Interfaces;
using ProductManagment.Domain.Entities;
using ProductManagment.WebUI.Models;

namespace ProductManagment.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserApiController> _logger;
        public UserApiController(IUserService userService, ILogger<UserApiController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers() => Ok(await _userService.GetAll());

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUser(int id) => Ok(await _userService.GetValueById(id));


        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] UserWithPasswordDTO userModel) 
        {
            _logger.LogInformation($"Post /UserApi/add/AddUser: Login={userModel.Login}, SystemRole={userModel.SystemRole}, Email={userModel.Email}");
            try
            {
                await _userService.Add(userModel);
                return Ok(new { success = true });
            }
            catch (InvalidOperationException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Post /UserApi/add/AddUser: Login={userModel.Login}, SystemRole={userModel.SystemRole}, Email={userModel.Email}: {ex.Message}");
                return BadRequest();
            }
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserWithPasswordDTO userModel)
        {
            _logger.LogInformation($"Post /UserApi/update/UpdateUser: Login={userModel.Login}, SystemRole={userModel.SystemRole}");

            await _userService.UpdatePasswordAsync(userModel.Id, userModel.Password);
            return Ok(new { success = true });
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation($"Delete /UserApi/delete/{id}");
            await _userService.Delete(id);
            return Ok(new { success = true });
        }


        [HttpPatch("block/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            _logger.LogInformation($"Post /UserApi/block/{id}");
            await _userService.Block(id);
            return Ok(new { success = true });
        }
    }
}
