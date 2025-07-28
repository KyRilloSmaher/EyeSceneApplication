
using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.ApplicationUser;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using EyeScenceApp.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EyeScenceApp.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ResponseHandler _responsehandler;
        private readonly IApplicationUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;
        private readonly IMapper _mapper;

        public AuthenticationService(JwtSettings jwtSettings, IApplicationUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IEmailService emailService, IHttpContextAccessor httpContextAccessor, IUrlHelper urlHelper, ResponseHandler responsehandler, IMapper mapper)
        {
            _jwtSettings = jwtSettings;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelper;
            _responsehandler = responsehandler;
            _mapper = mapper;
        }
        public async Task<Response<JwtResponse>> Login(LoginDTO dto)
        {
            if (dto is null)
                return _responsehandler.BadRequest<JwtResponse>("Dto is Null");
            var ApplicationUser = await _userRepository.GetByEmailAsync(dto.email);
            if (ApplicationUser == null)
                return _responsehandler.Failed<JwtResponse>("Email or Passowrd is Wrong");
            var isValidPassword = await _userRepository.CheckPasswordAsync(ApplicationUser, dto.Password);
            if (!isValidPassword)
                return _responsehandler.Failed<JwtResponse>("Email or Passowrd is Wrong");
            if (!ApplicationUser.EmailConfirmed)
                return _responsehandler.Failed<JwtResponse>("Email is Not Confirmed yet");
            var jwt = await this.GetJWTToken(ApplicationUser);
            return _responsehandler.Success<JwtResponse>(jwt, "User Login SuccessFully");
        }

        public async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var roles = await _userRepository.GetApplicationUserRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(accessToken);
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new SecurityTokenException("Invalid token");

            var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (storedRefreshToken == null || storedRefreshToken.UserId != userId || storedRefreshToken.ExpiryDate > DateTime.UtcNow)
                throw new SecurityTokenException("Invalid refresh token");

            return (userId, storedRefreshToken.ExpiryDate);
        }

        public async Task<JwtResponse> RetrieveRefreshToken(ApplicationUser user, JwtSecurityToken jwtToken, string refreshToken, DateTime? expiryDate = null)
        {
            // Validate the old refresh token
            var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (storedRefreshToken == null)
                throw new SecurityTokenException("Invalid refresh token");

            // Mark the old refresh token as used
            await _refreshTokenRepository.MarkTokenAsUsedAsync(refreshToken);

            // Clean up expired tokens
            await _refreshTokenRepository.DeleteExpiredTokensAsync();

            // Generate new tokens
            var newTokens = await GetJWTToken(user);

            // Revoke the old refresh token
            await _refreshTokenRepository.RevokeTokenAsync(refreshToken);

            return newTokens;
        }
        public async Task<JwtResponse> GetJWTToken(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var claims = await GetClaims(user);
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = new RefreshToken
            {
                UserName = user.UserName,
                TokenString = Guid.NewGuid().ToString(),
                ExpireAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDate),
            };

            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = false,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id
            };

            await _refreshTokenRepository.AddAsync(userRefreshToken);
            var rs= new JwtResponse
            {
                refreshToken = refreshToken,
                AccessToken = accessToken
            };
            return  rs;
        }
        public async Task<bool> ValidateToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            try
            {
                tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Response<JwtResponse>> GetRefreshToken(RefreshTokenCommandDTO request)
        {
            // Step 1: Validate Access Token
            var token =  ReadJWTToken(request.AccessToken);
            if (token == null || token.ValidTo < DateTime.UtcNow)
                return _responsehandler.BadRequest<JwtResponse>("Invalid Access Token");

            // Step 2: Validate Refresh Token
            if (string.IsNullOrEmpty(request.RefreshToken))
                return _responsehandler.BadRequest<JwtResponse>("Invalid Refresh Token");

            // Step 3: Extract User ID
            var userId = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return _responsehandler.BadRequest<JwtResponse>("Invalid User ID");

            // Step 4: Verify User Exists
            var user = await  _userRepository.GetByIdAsync(userId);
            if (user == null)
                return _responsehandler.BadRequest<JwtResponse>("User Not Found");

            // Step 5: Validate Token Details
            var (validatedUserId, expiryDate) = await ValidateDetails(
                token,
                request.AccessToken,
                request.RefreshToken
            );

            // Step 6: Verify User IDs Match
            if (validatedUserId != userId)
                return _responsehandler.BadRequest<JwtResponse>("User ID Mismatch");

            // Step 7: Generate New Tokens
            var newTokens = await RetrieveRefreshToken(
                user,
                token,
                request.RefreshToken,
                expiryDate
            );

            // Step 8: Return New Tokens
            return _responsehandler.Success(newTokens);
        }
        
        
        public async Task<Response<bool>> ConfirmEmailAsync(ConfirmEmailDTO dto)
        {
            var isValidToken = await _userRepository.ConfirmEmailAsync(dto.email,dto.Code);
            if (!isValidToken)
                return _responsehandler.Success<bool>(false , "Email does not Confirmed");

            return _responsehandler.Success<bool>(true, "Email Is Confirmed"); ;
        }
        public async Task<Response<bool>> SendResetPasswordCode(SendResetCodeCommandModel model)
        {

            try
            {
                await _userRepository.BeginTransactionAsync();
                //user
                var user = await _userRepository.GetByEmailAsync(model.Email, true);
                //user not Exist => not found
                if (user == null)
                    return _responsehandler.Failed<bool>("User Not Found");
                //Generate Random Number
                Random generator = new Random();
                string randomNumber = generator.Next(0, 1000000).ToString("D6");

                //update User In Database Code
                user.code = randomNumber;
                var updateResult = await _userRepository.UpdateApplicationUserAsync(user);
                if (!updateResult.Succeeded)
                    return _responsehandler.Failed<bool>("Failed To Generate Code");
                var code = user.code;
                //Send Code To  Email 
                await _emailService.SendPasswordResetEmailAsync(user.Email, "Reset Password", code);
                await _userRepository.CommitTransactionAsync();
                return _responsehandler.Success<bool>(true,"Reset passowrd Code Send Successfully");
            }
            catch (Exception ex)
            {
                await _userRepository.RollbackTransactionAsync();
                return _responsehandler.Failed<bool>("Failed To Generate reset password Code"); ;
            }
        }
        public async Task<Response<bool>> ConfirmResetPassword(ConfirmResetPasswordCodeDTO dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.email);

            if (user == null)
                return _responsehandler.Failed<bool>("Email is Wrong");
            var userCode = user.code;
            if (userCode != dto.Code) return _responsehandler.Failed<bool>("Code is Wrong");
            return _responsehandler.Success<bool>(true , "Reset password Code Confirmed");
        }
        public async Task<Response<bool>> ResetPassword(ResetPasswordDTO dto)
        {
            try
            {
                await _userRepository.BeginTransactionAsync();
                var user = await _userRepository.GetByEmailAsync(dto.Email, true);
                if (user == null)
                    return _responsehandler.Failed<bool>("Email is Wrong");
                await _userRepository.RemovePasswordAsync(user);
                if (!await _userRepository.HasPasswordAsync(user))
                {
                    await _userRepository.AddPasswordAsync(user, dto.NewPassword);
                }
                await _userRepository.CommitTransactionAsync();
                return _responsehandler.Success<bool>(true, "password reset Successfuly");
            }
            catch (Exception ex)
            {
                await _userRepository.RollbackTransactionAsync();
                return _responsehandler.Failed<bool>("Error occured during reset passsord");
            }
        }
        public async Task<Response<bool>> ChangePasswordAsync(ChangePasswordDTO dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email, true);
            if (user == null)
                return _responsehandler.Failed<bool>("Email is Wrong");
            var result = await _userRepository.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
            if (!result)
                return _responsehandler.Failed<bool>("Failed to chage password");
            return _responsehandler.Success<bool>(true, "password Chnaged Successfuly");
        }

    }
}
