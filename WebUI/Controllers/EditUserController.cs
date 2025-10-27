using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.DTOs;
using ProductManagment.Domain.Interfaces;
using ProductManagment.WebUI.Models;

namespace ProductManagment.WebUI.Controllers
{
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

        //public IActionResult Update(CreateUserModel userModel)
        //{
        //    _userService.Update(GetProductDTO(userModel));
        //    return RedirectToAction("User", "Users");
        //}

        private UserWithPasswordDTO GetProductDTO(CreateUserModel userModel)
        {
            return new UserWithPasswordDTO(userModel.Id,userModel.Login, userModel.SystemRoleId, userModel.SystemRole, userModel.Email, userModel.Password);
        }
    }
}
