using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.ApplicationUser;
using EyeScenceApp.Application.IServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(Router.UserAccount.GetAllUsers)]

        public async Task<IActionResult> GetAllUserAccounts()
        {
            var response = await _userService.GetAllApplicationUsersAsync();
            return Router.FinalResponse(response);
        }
        [HttpGet(Router.UserAccount.GetAllAdmins)]

        public async Task<IActionResult> GetAllAdmins()
        {
            var response = await _userService.GetAllAdmins();
            return Router.FinalResponse(response);
        }
        [HttpGet(Router.UserAccount.GetById)]
        public async Task<IActionResult> GetUserAccountById([FromRoute] string userId)
        {
            var response = await _userService.GetApplicationUserByIdAsync(userId);
            return Router.FinalResponse(response);
        }
        //[HttpGet(Router.UserAccount.GetByUsername)]

        //public async Task<IActionResult> GetUserAccountByEmail([FromRoute] string UserAccountname)
        //{
        //    var response = await _userService.getAppl
        //        (new GetUserAccountByUserAccountNameQueryModel(UserAccountname));
        //    return Router.FinalResponse(response);
        //}
        //[HttpPost(Router.UserAccount.RegisterAdmin)]
        //public async Task<IActionResult> RegisterAdmin([FromBody] CreateAdminCommandModel query)
        //{
        //    var response = await_userService(query);
        //    return Router.FinalResponse(response);
        //}
        [HttpPost(Router.UserAccount.RegisterUser)]
        public async Task<IActionResult> RegisterUserAccount([FromBody] CreateApplicationUserDTO dto)
        {
            var response = await _userService.AddApplicationUser(dto);
            return Router.FinalResponse(response);
        }
        [HttpPut(Router.UserAccount.UpdateUser)]
        public async Task<IActionResult> UpdateUserAccount([FromBody] UpdateUserDTO dto)
        {
            var response = await _userService.UpdateApplicationUserAsync(dto);
            return Router.FinalResponse(response);
        }
        [HttpDelete(Router.UserAccount.DeleteUser)]
        public async Task<IActionResult> DeleteUserAccount([FromRoute] string Id)
        {
            var response = await _userService.DeleteApplicationUserAsync(Id);
            return Router.FinalResponse(response);
        }
    }
}
