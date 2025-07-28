using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using EyeScenceApp.Domain.Shared;
using EyeScenceApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Infrastructure.Repositories
{
    public class ResfreshTokenRepository : GenericRepository<UserRefreshToken> ,IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public ResfreshTokenRepository(ApplicationDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserRefreshToken> GetByTokenAsync(string refreshToken)
        {
            return await _context.UserRefreshTokens
                .FirstOrDefaultAsync(rt => rt.RefreshToken == refreshToken);
        }

        public async Task<UserRefreshToken> GetByJwtIdAsync(string jwtId)
        {
            return await _context.UserRefreshTokens
                .FirstOrDefaultAsync(rt => rt.JwtId == jwtId);
        }

        public async Task<bool> MarkTokenAsUsedAsync(string refreshToken)
        {
            var token = await GetByTokenAsync(refreshToken);
            if (token == null) return false;

            token.IsUsed = true;
            _context.UserRefreshTokens.Update(token);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RevokeTokenAsync(string refreshToken)
        {
            var token = await GetByTokenAsync(refreshToken);
            if (token == null) return false;

            token.IsRevoked = true;
            _context.UserRefreshTokens.Update(token);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteExpiredTokensAsync()
        {
            var expiredTokens = _context.UserRefreshTokens
                .Where(rt => rt.ExpiryDate < DateTime.UtcNow);

            _context.UserRefreshTokens.RemoveRange(expiredTokens);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsTokenValid(string refreshToken)
        {
            var token = await GetByTokenAsync(refreshToken);
            if (token == null) return false;

            return !token.IsUsed &&
                   !token.IsRevoked &&
                   token.ExpiryDate >= DateTime.UtcNow;
        }
    }
}
