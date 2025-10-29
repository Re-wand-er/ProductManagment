using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.DTOs;
using ProductManagment.Application.Interfaces;
using ProductManagment.WebUI.Models;

namespace ProductManagment.WebUI.Controllers
{
    [Authorize]
    public class EditUserController : Controller
    {
        private readonly IUserService _userService;
        public EditUserController(IUserService userService)
        {
            this._userService = userService;
        }

        public async Task<IActionResult> Create(CreateUserModel userModel)
        {
            await _userService.Add(GetProductDTO(userModel));
            return RedirectToAction("Users", "User");
        }

        public async Task<IActionResult> Update(CreateUserModel userModel)
        {
            await _userService.UpdatePasswordAsync(userModel.Id, userModel.Password);
            return RedirectToAction("Users", "User");
        }

        private UserWithPasswordDTO GetProductDTO(CreateUserModel userModel)
        {
            return new UserWithPasswordDTO(userModel.Id,userModel.Login, userModel.SystemRoleId, userModel.SystemRole, userModel.Email, userModel.Password);
        }
    }
}
