using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.DTOs;
using ProductManagment.Application.Interfaces;

namespace ProductManagment.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserApiController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers() => Ok(await _userService.GetAll());

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUser(int id) => Ok(await _userService.GetValueById(id));


        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] UserWithPasswordDTO userModel) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); //_logger

            await _userService.Add(userModel);
            return Ok(new { success = true });
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserWithPasswordDTO userModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // _logger

            await _userService.UpdatePasswordAsync(userModel.Id, userModel.Password);
            return Ok(new { success = true });
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.Delete(id);
            return Ok(new { success = true });
        }


        [HttpPatch("block/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            await _userService.Block(id);
            return Ok(new { success = true });
        }
    }
}
