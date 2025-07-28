

namespace EyeScenceApp.Application.DTOs.ApplicationUser
{
    public class RefreshTokenCommandDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
