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
    public class WatchListRepository : GenericRepository<WatchList>,IWatchListRepository
    {
        #region Field(s)
        private readonly ApplicationDbContext _context;
        #endregion
        #region Constructor(s)
        public WatchListRepository(ApplicationDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "ApplicationDbContext cannot be null.");
        }
        #endregion
        #region Method(s)
        public async Task<IEnumerable<WatchList>> GetWatchListByUserIdAsync(string userId)
        {
            return await _context.WatchLists.Include(w=>w.DigitalContent).ThenInclude(d=>d.Image)
                                            .Include(w=>w.DigitalContent).ThenInclude(d=>d.Genres).ThenInclude(g=>g.Genre)
                                            .Where(w => w.UserId == userId).ToListAsync();
        }
     
        public async Task<bool> ClearWatchListAsync(string userId)
        {
            var watchLists = await _context.WatchLists.Where(w => w.UserId == userId).ToListAsync();
            if (watchLists.Count == 0) return false;

            _context.WatchLists.RemoveRange(watchLists);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsInWatchListAsync(string userId, Guid DigitalContentId)
        {
            return await _context.WatchLists.AnyAsync(w => w.UserId == userId && w.DigitalContentId == DigitalContentId);
        }
        #endregion
    }
}
