using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using EyeScenceApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Infrastructure.Repositories
{
    public class FavoriteRepository : GenericRepository<Favorite>,IFavoriteRepository
    {
        #region Field(s)
        private readonly ApplicationDbContext _context;
        #endregion
        #region Constructor(s)
        public FavoriteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "ApplicationDbContext cannot be null.");
        }
        #endregion
        #region Method(s)
        public async Task<IEnumerable<Favorite>> GetFavoritesByUserIdAsync(string userId)
        {
            return await _context.Favorites
                .Include(f => f.DigitalContent)
                    .ThenInclude(d => d.Image)
                .Include(f => f.DigitalContent)
                    .ThenInclude(d => d.Genres)
                    .ThenInclude(g=>g.Genre)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> ClearFavoritesAsync(string userId)
        {
            var favorites = await _context.Favorites.Where(f => f.UserId == userId).ToListAsync();
            if (favorites.Count == 0) return false;

            _context.Favorites.RemoveRange(favorites);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> IsFavoriteAsync(string userId, Guid DigitalContentId)
        {
            return await _context.Favorites.AnyAsync(f => f.UserId == userId && f.DigitalContentId == DigitalContentId);
        }

        #endregion
    }
}
