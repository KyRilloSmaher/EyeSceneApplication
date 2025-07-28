using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using EyeScenceApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Infrastructure.Repositories
{
    public class SeriesRepository : GenericRepository<Series>, ISeriesRepository
    {
        #region Field(s)
        private readonly ApplicationDbContext _dbcontext;
        private readonly IRatingRepository _ratingRepository;
        #endregion

        #region Constructor(s)
        public SeriesRepository(ApplicationDbContext context, IRatingRepository ratingRepository) : base(context)
        {
            _dbcontext = context ?? throw new ArgumentNullException(nameof(context), "ApplicationDbContext cannot be null.");
            _ratingRepository = ratingRepository;
        }
        #endregion

        #region Method(s)
        public async Task<IEnumerable<Series?>> GetSeriesByTitleAsync(string title)
        {
            return await _dbcontext.Series
                .Include(s => s.Genres)
                    .ThenInclude(g => g.Genre)
                .Include(s => s.Image)
                .Where(s => s.Title.Contains(title.ToLower()))
                .ToListAsync();
        }

        public async Task<IEnumerable<Series?>> GetSeriesByGenreIdAsync(byte genreId)
        {
            if (genreId == 0)
            {
                throw new ArgumentException("Genre ID cannot be zero.", nameof(genreId));
            }

            return await _dbcontext.Series
                                    .Include(sd => sd.Genres)
                                        .ThenInclude(g => g.Genre)
                                    .Include(sd => sd.Image)
                                   .Where(dg => dg.Genres.Any(genre => genre.GenreId == genreId))
                                   .ToListAsync();
        }

        public async Task<IEnumerable<Series?>> GetNewAddedSeriesAsync(byte genreId = 0)
        {   // Calculate the date one month ago from today
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);

            IQueryable<Series> query = _dbcontext.Series
                .Include(sd => sd.Genres)
                    .ThenInclude(g => g.Genre)
                .Include(sd => sd.Image)
                .Where(m => m.UploadingDate >= oneMonthAgo); // Only include Series from last month

            if (genreId > 0)
            {
                query = query.Where(m => m.Genres.Any(g => g.GenreId == genreId));
            }

            return await query.OrderByDescending(m => m.UploadingDate)
                             .Take(10)
                             .ToListAsync();
        }

        public async Task<IEnumerable<Series?>> GetTopRatedSeriesAsync(int topCount = 10)
        {
            var Series = await _dbcontext.Series
                                           .Include(m => m.Genres).ThenInclude(g => g.Genre)
                                           .Include(m => m.Image)
                                           .ToListAsync();
            foreach (var serie in Series)
            {
                serie.Rate = _ratingRepository.GetRatesOfDigitalContentAsync(serie.Id);
            }
            return Series.OrderByDescending(x => x.Rate).Take(topCount);
        }
        public async override Task<IQueryable<Series>> FilterListAsync<TKey>(Expression<Func<Series, TKey>> orderBy, Expression<Func<Series, bool>> searchPredicate = null,bool ascending = true)
        {
            IQueryable<Series> query = _dbContext.Series.Include(sd => sd.Genres).ThenInclude(g => g.Genre)
                                                         .Include(a => a.Image);


            if (searchPredicate != null)
            {
                query = query.Where(searchPredicate);
            }

            query = ascending ?
                query.OrderBy(orderBy) :
                query.OrderByDescending(orderBy);

            return await Task.FromResult(query);
        }
        public async override Task<Series?> GetByIdAsync(Guid Id, bool AsTracked = false)
        {
            return AsTracked ? await _dbcontext.Series.Include(sd => sd.Genres).ThenInclude(g => g.Genre)
                                                                    .Include(sd => sd.Image).AsTracking().FirstOrDefaultAsync(sd => sd.Id == Id)
                              :
                                await _dbcontext.Series.Include(sd => sd.Genres).ThenInclude(g => g.Genre)
                                                                    .Include(sd => sd.Image).AsNoTracking().FirstOrDefaultAsync(sd => sd.Id == Id);
        }
        public async override Task<IEnumerable<Series>> GetAllAsync(bool AsTracking = false)
        {
            return AsTracking ? await _dbcontext.Series.Include(sd => sd.Genres).ThenInclude(g => g.Genre)
                                                                    .Include(sd => sd.Image).AsTracking().ToListAsync()
                              :
                                await _dbcontext.Series.Include(sd => sd.Genres).ThenInclude(g => g.Genre)
                                                                    .Include(sd => sd.Image).AsNoTracking().ToListAsync();
        }
      
            #endregion
        }
}
