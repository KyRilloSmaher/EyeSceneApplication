using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.ApplicationUser;
using EyeScenceApp.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;


namespace EyeScenceApp.Application.IServices
{
    public interface IAuthenticationService
    {
        Task<JwtResponse> GetJWTToken(ApplicationUser user);
        JwtSecurityToken ReadJWTToken(string accessToken);
        Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken);
        Task<JwtResponse>RetrieveRefreshToken(ApplicationUser user, JwtSecurityToken jwtToken, string refreshToken, DateTime? expiryDate = null);

        Task<Response<JwtResponse>> GetRefreshToken(RefreshTokenCommandDTO dto);
        Task<bool> ValidateToken(string accessToken);
        Task<Response<bool>> ConfirmEmailAsync(ConfirmEmailDTO dto);
        Task<Response<bool>> SendResetPasswordCode(SendResetCodeCommandModel dto);
        Task<Response<bool>> ConfirmResetPassword(ConfirmResetPasswordCodeDTO dto);
        Task<Response<bool>> ResetPassword(ResetPasswordDTO dto);
        Task<Response<bool>> ChangePasswordAsync(ChangePasswordDTO dto);
        Task<Response<JwtResponse>> Login(LoginDTO dto);

    }
}
