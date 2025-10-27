using Microsoft.AspNetCore.Mvc;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.WebUI.API.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserApiController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers() 
        {
            return Ok(await _userService.GetAll());
        }
    }
}
