using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.ApplicationUser;
using EyeScenceApp.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }
        [HttpGet(Router.Authentication.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery]ConfirmEmailDTO dto)
        {
            var response = await _authenticationService.ConfirmEmailAsync(dto);
            if (response.Succeeded)
            {
                return Content("<h1>Email Confirmed Successfully !</h1>", "text/html");
            }
            return Content("<h1>Invalid or Expired Confirmation Email !</h1>", "text/html");
        }
        [HttpPost(Router.Authentication.Login)]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var response = await _authenticationService.Login(dto);
            return Router.FinalResponse(response);
        }
        [HttpPost(Router.Authentication.SendResetCode)]
        public async Task<IActionResult> SendResetCode([FromBody] SendResetCodeCommandModel model)
        {
            var response = await _authenticationService.SendResetPasswordCode(model);
            return Router.FinalResponse(response);
        }
        [HttpPost(Router.Authentication.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            var response = await _authenticationService.ResetPassword(dto);
            return Router.FinalResponse(response);
        }
        [HttpPost(Router.Authentication.ConfirmResetPasswordCode)]
        public async Task<IActionResult> ConfirmResetPassword([FromBody] ConfirmResetPasswordCodeDTO dto)
        {
            var response = await _authenticationService.ConfirmResetPassword(dto);
            return Router.FinalResponse(response);
        }
        [Authorize]
        [HttpPost(Router.Authentication.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            var response = await _authenticationService.ChangePasswordAsync(dto);
            return Router.FinalResponse(response);
        }
        [Authorize]
        [HttpPost(Router.Authentication.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommandDTO dto)
        {
            var response = await _authenticationService.GetRefreshToken(dto);
            return Router.FinalResponse(response);
        }
    }
}
