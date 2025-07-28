using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using EyeScenceApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace EyeScenceApp.Infrastructure.Repositories
{
    public class WritersRepository : GenericRepository<Writer>, IWriterRepository
    {
        #region Field(s)
        private readonly ApplicationDbContext _dbContext;
        #endregion

        #region Constructor(s)
        public WritersRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext ?? throw new ArgumentNullException(nameof(_dbContext), "ApplicationDbContext cannot be null.");

        }
        #endregion

        #region Method(s)
        public async override Task<IEnumerable<Writer>> GetAllAsync(bool isTracked = false)
        {
            var query = _dbContext.Writers.Include(d => d.Images)
                                                        .ThenInclude(imges => imges.Image);

            return isTracked
                ? await query.ToListAsync()
                : await query.AsNoTracking().ToListAsync();
        }
        public async override Task<Writer?> GetByIdAsync(Guid id, bool AsTracking = false)
        {
            var query = _dbContext.Writers.Include(d => d.Images)
                                                        .ThenInclude(imges => imges.Image)
                                             .Include(d => d.Awards)
                                                         .ThenInclude(award => award)

                                                     .AsQueryable();

            if (!AsTracking)
                query = query.AsNoTracking();

            var result = await query.FirstOrDefaultAsync(a => a.Id == id);
            return result;

        }
        public async Task<IEnumerable<Writer>> GetByNameAsync(string name, bool isTracked = false)
        {
            var query = _dbContext.Writers
                .Where(c => c.FirstName.Contains(name) ||
                            c.LastName.Contains(name) ||
                            c.MiddleName.Contains(name));
            return isTracked
                ? await query.ToListAsync()
                : await query.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<DigitalContent>> GetAllWorksAsync(Guid crewId)
        {
            return await _dbContext.WorksOn.Include(w => w.DigitalContent).ThenInclude(i => i.Image)
                                            .Include(w => w.DigitalContent).ThenInclude(d => d.Genres).ThenInclude(g => g.Genre)
                                            .Where(w => w.CrewId == crewId)
                                            .Select(w => w.DigitalContent)
                                            .ToListAsync();
        }

   

        #endregion
    }
}
