using CloudinaryDotNet;
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
    public class SingleDocumentaryRepository : GenericRepository<SingleDocumentary>,ISingleDocumentaryRepository
    {
        #region Field(s)
        private readonly ApplicationDbContext _dbcontext;
        private readonly IRatingRepository _ratingRepository;
        #endregion

        #region Constructor(s)
        public SingleDocumentaryRepository(ApplicationDbContext context, IRatingRepository ratingRepository) : base(context)
        {
            _dbcontext = context ?? throw new ArgumentNullException(nameof(context), "ApplicationDbContext cannot be null.");
            _ratingRepository = ratingRepository;
        }
        #endregion

        #region Method(s)
        public async override Task<SingleDocumentary?> GetByIdAsync(Guid Id , bool AsTracked = false)
        {
            return  AsTracked ? await _dbcontext.SingleDocumentaries.Include(sd => sd.Genres).ThenInclude(g => g.Genre)
                                                                    .Include(sd => sd.Image).AsTracking().FirstOrDefaultAsync(sd => sd.Id == Id)
                              :
                                await _dbcontext.SingleDocumentaries.Include(sd => sd.Genres).ThenInclude(g => g.Genre)
                                                                    .Include(sd => sd.Image).AsNoTracking() .FirstOrDefaultAsync(sd => sd.Id == Id);
        }
        public async Task<IEnumerable<SingleDocumentary>> GetSingleDocumentaryByTitleAsync(string title)
        {
            var singleDocumentaries = await _dbcontext.SingleDocumentaries
                 .Include(sd => sd.Genres)
                     .ThenInclude(g => g.Genre)
                 .Include(sd => sd.Image)
                 .Where(sd => sd.Title.Contains(title.ToLower()) || sd.ShortDescription.Contains(title))
                 .ToListAsync();
            return singleDocumentaries;
        }

        public async Task<IEnumerable<SingleDocumentary>> GetSingleDocumentariesByGenreIdAsync(byte genreId)
        {
            return await _dbcontext.SingleDocumentaries
                .Include(sd => sd.Genres)
                    .ThenInclude(g => g.Genre)
                .Include(sd => sd.Image)
                .Where(sd => sd.Genres.Any(g => g.GenreId == genreId))
                .ToListAsync();
        }

        public async Task<IEnumerable<SingleDocumentary>> GetNewAddedSingleDocumentaries(byte genreId = 0)
        {
            // Calculate the date one month ago from today
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);

            IQueryable<SingleDocumentary> query = _dbcontext.SingleDocumentaries
                .Include(sd => sd.Genres)
                    .ThenInclude(g => g.Genre)
                .Include(sd => sd.Image)
                .Where(m => m.UploadingDate >= oneMonthAgo); // Only include movies from last month

            if (genreId > 0)
            {
                query = query.Where(m => m.Genres.Any(g => g.GenreId == genreId));
            }

            return await query.OrderByDescending(m => m.UploadingDate)
                             .Take(10)
                             .ToListAsync();
        }
        public async override Task<IQueryable<SingleDocumentary>> FilterListAsync<TKey>(
                Expression<Func<SingleDocumentary, TKey>> orderBy,
                Expression<Func<SingleDocumentary, bool>> searchPredicate = null,
                bool ascending = true)
                    {
            IQueryable<SingleDocumentary> query = _dbContext.SingleDocumentaries.Include(sd => sd.Genres).ThenInclude(g => g.Genre)
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
        public async Task<IEnumerable<SingleDocumentary>> GetTopRatedSingleDocumantaryAsync(int topCount = 10)
        {
            var documentaries = await _dbcontext.SingleDocumentaries
                                .Include(m => m.Genres).ThenInclude(g => g.Genre)
                                .Include(m => m.Image)
                                .ToListAsync();
            foreach (var documentary in documentaries)
            {
                documentary.Rate = _ratingRepository.GetRatesOfDigitalContentAsync(documentary.Id);
            }
            return documentaries.OrderByDescending(x => x.Rate).Take(topCount);
        }
        #endregion
    }
}
