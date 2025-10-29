using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ProductManagment.Application.Interfaces;
using ProductManagment.Application.Services;
using ProductManagment.Domain.Interfaces;
using ProductManagment.Infrastructure.Persistence.Repositories;
using ProductManagment.WebUI.Models;

namespace ProductManagment.WebUI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleRepository _roleRepository;
        public UserController(IUserService userService, IRoleRepository roleRepository)
        {
            this._userService = userService;
            this._roleRepository = roleRepository;
        }
        public async Task<IActionResult> Users() => View("User", await GetUsersAsync());

        public async Task<IActionResult> Create() 
        {
            var createUser = new CreateUserModel() { Roles = await GetRolesAsync() };
            return View("EditUser", createUser);
        }

        public async Task<IActionResult> Edit(int id)
        {
            CreateUserModel createUser;

            var roles = await GetRolesAsync();
            var result = await _userService.GetValueById(id);

            if (result != null)
            {
                createUser = new CreateUserModel()
                {
                    Id = result.Id,
                    Login = result.Login,
                    SystemRole = result.SystemRole,
                    SystemRoleId = result.SystemRoleId,
                    Roles = roles,
                    Email = result.Email
                };
            }
            else
            {
                createUser = new CreateUserModel() { Roles = roles };
            }

            return View("EditUser", createUser);
        }

        public async Task<IActionResult> Block(int id) 
        {
            await _userService.Block(id);
            return View("User", await GetUsersAsync());
        }

        public async Task<IActionResult> Delete(int id) 
        {
            await _userService.Delete(id);
            return View("User", await GetUsersAsync());
        }

        private async Task<List<RoleModel>> GetRolesAsync()
        {
            var categoriesEntity = await _roleRepository.GetAllAsync();
            return categoriesEntity.Select(c => new RoleModel() { Id = c.Id, Name = c.Name }).ToList();
        }

        private async Task<IEnumerable<UserModel>> GetUsersAsync() 
        {
            var userEntity = await _userService.GetAll();
            return userEntity.Select(u => new UserModel() { Id = u.Id, Login = u.Login, SystemRole = u.SystemRole, Email = u.Email, IsBlocked = u.IsBlocked }).ToList();
        }

        //public async Task<IActionResult> Users()
        //{
        //    //var usersDTO = await GetObjectListAsync<UserDTO>("api/UserApi");
        //    var usersDTO = await _productService.GetAll();
        //    var users = usersDTO?
        //        .Select(u => new UserModel(u.Login, u.SystemRole, u.Email))
        //        .ToList();
        //
        //    return View("User", users);
        //}
    }
}
