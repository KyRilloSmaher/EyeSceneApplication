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
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        #region Field(s)
        private readonly ApplicationDbContext _dbcontext;
        #endregion

        #region Constructor(s)
        public GenreRepository(ApplicationDbContext context) : base(context)
        {
            _dbcontext = context ?? throw new ArgumentNullException(nameof(context), "ApplicationDbContext cannot be null.");
        }
        #endregion

        #region Method(s)

        public async Task<Genre> GetByIdAsync(byte Id , bool asTracked =false) {
            var result = asTracked ?     await _dbContext.Genres.Include(g => g.Image).FirstOrDefaultAsync(a => a.Id == Id)
                                       : await _dbContext.Genres.Include(g => g.Image).AsNoTracking().FirstOrDefaultAsync(a => a.Id == Id);
            return result;
        }
        public async Task<Genre> GetByNameAsync(string name, bool asTracked = false)
        {
            var result = asTracked ?   await _dbContext.Genres.Include(g => g.Image).FirstOrDefaultAsync(a => a.Name == name)
                                     : await _dbContext.Genres.Include(g => g.Image).AsNoTracking().FirstOrDefaultAsync(a => a.Name == name);
            return result;
        }
        public override async Task<IEnumerable<Genre>> GetAllAsync(bool asTracked = false)
        {
            var result = asTracked ? await _dbContext.Genres.Include(g => g.Image).ToListAsync()
                                   : await _dbContext.Genres.Include(g => g.Image).AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<bool> IsvalidGenre(byte id)
        {
            return await _dbcontext.Genres.AnyAsync(g => g.Id == id);
        }
        #endregion
    }
}
