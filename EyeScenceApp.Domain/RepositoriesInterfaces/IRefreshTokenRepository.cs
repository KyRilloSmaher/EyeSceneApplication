using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface IRefreshTokenRepository : IGenericRepository<UserRefreshToken>
    {
        Task<UserRefreshToken> GetByTokenAsync(string refreshToken);
        Task<UserRefreshToken> GetByJwtIdAsync(string jwtId);
        Task<bool> MarkTokenAsUsedAsync(string refreshToken);
        Task<bool> RevokeTokenAsync(string refreshToken);
        Task<bool> DeleteExpiredTokensAsync();
        Task<bool> IsTokenValid(string refreshToken);
    }
}
