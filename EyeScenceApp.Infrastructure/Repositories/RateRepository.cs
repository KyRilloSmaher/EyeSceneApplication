using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using EyeScenceApp.Infrastructure.DbContext;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Infrastructure.Repositories
{
    public class RateRepository : GenericRepository<Rate>, IRatingRepository
    {
        #region Feild(s)
        private readonly ApplicationDbContext _DbContext;
        #endregion

        #region Constructor(s)
        public RateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "ApplicationDbContext cannot be null.");
        }

        #endregion

        #region Method(s)
        public async override Task<Rate?> GetByIdAsync(Guid rateId ,bool AsTracked = false)
        {
            return AsTracked ? await _DbContext.Rates.Include(r => r.DigitalContent).Include(r => r.User).AsTracking().FirstOrDefaultAsync(r => r.Id == rateId)
                             : await _DbContext.Rates.Include(r => r.DigitalContent).Include(r => r.User) .AsNoTracking().FirstOrDefaultAsync(r => r.Id == rateId);
        }
        public override async Task<Rate> AddAsync(Rate rate)
        {
            await _dbContext.Rates.AddAsync(rate);
            await _dbContext.SaveChangesAsync();

            var digitalContent = await _dbContext.DigitalContents
                .FirstOrDefaultAsync(d => d.Id == rate.DigitalContentId);

            if (digitalContent != null)
            {
                // Calculate the new average rate for this digital content
                var avgRate = await _dbContext.Rates
                    .Where(r => r.DigitalContentId == rate.DigitalContentId)
                    .AverageAsync(r => r.Value);

                digitalContent.Rate = avgRate;
                await _dbContext.SaveChangesAsync();
            }

            return rate;
        }
        public override async Task DeleteAsync(Rate rate)
        {
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                _dbContext.Rates.Remove(rate);
                var digitalContent = await _dbContext.DigitalContents.FirstOrDefaultAsync(d => d.Id == rate.DigitalContentId);

                if (digitalContent != null)
                {
                    // Calculate the new average rate for this digital content
                    var avgRate = await _dbContext.Rates
                        .Where(r => r.DigitalContentId == rate.DigitalContentId)
                        .AverageAsync(r => r.Value);

                    digitalContent.Rate = avgRate;
                    await _dbContext.SaveChangesAsync();
                    await _dbContext.Database.CommitTransactionAsync();
                }

                else
                    throw new NullReferenceException("Digital Content NotFound");
            }
            catch (Exception ex)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                throw new Exception("");
            }

        }
        public async Task<IEnumerable<Rate>> GetAllRatesByUserAsync(string userId, DigitalContent? digitalContentType = null)
        {
            var query = _DbContext.Rates
                .Include(r => r.DigitalContent)
                .Include(r => r.User)
                .Where(r => r.UserId == userId);

            if (digitalContentType != null)
            {
                // Get the actual type of the digitalContentType parameter
                Type contentType = digitalContentType.GetType();
                query = query.Where(r => r.DigitalContent != null &&
                                       contentType.IsInstanceOfType(r.DigitalContent));
            }

            return await query.ToListAsync();
        }
        public double GetRatesOfDigitalContentAsync(Guid id)
        {
            var DgCont = _dbContext.DigitalContents.FirstOrDefault(c => c.Id == id);
            if (DgCont == null) return -0.1;

            return _dbContext.Rates.Where(dg => dg.DigitalContentId == id).Average(dg => dg.Value);
        }
        public async Task<IEnumerable<Rate>> GetAllRatesOfDigitalContentAsync(Guid id)
        {
            return await _dbContext.Rates.Include(r=>r.DigitalContent).Include(r=>r.User).Where(dg => dg.DigitalContentId == id).ToListAsync();
        }
        public async Task<IEnumerable<DigitalContent>> GetTopRatedDigitalContentsAsync(int topCount = 10)
        {
            var topRatedDigitalContents = await _dbContext.DigitalContents
                .Include(dc => dc.Genres)
                    .ThenInclude(g => g.Genre)
                .Include(dc => dc.Image)
                .Select(dc => new
                {
                    DigitalContent = dc,
                    AverageRating = dc.Ratings.Average(r => (double?)r.Value) ?? 0 // Handle no ratings
                })
                .OrderByDescending(x => x.AverageRating)
                .Take(topCount)
                .Select(x => x.DigitalContent)
                .ToListAsync();

            return topRatedDigitalContents;
        }

        public async  Task<IEnumerable<DigitalContent>> GetTopRatedDigitalContentsByGenreAsync(string genreName, int topCount = 10)
        {
            var topRatedDigitalContents = await _dbContext.DigitalContents
               .Include(dc => dc.Genres)
                   .ThenInclude(g => g.Genre)
               .Include(dc => dc.Image)
               .Select(dc => new
               {
                   DigitalContent = dc,
                   AverageRating = dc.Ratings.Average(r => (double?)r.Value) ?? 0 // Handle no ratings
               })
               .Where(x => x.DigitalContent.Genres.Any(g => g.Genre.Name == genreName)) // Filter by genre name
                .OrderByDescending(x => x.AverageRating)
                .Take(topCount)
                .ToListAsync();

            return topRatedDigitalContents.Select(x => x.DigitalContent);
        }

        public async Task<bool> IncreamentRateLikeAsync(Guid rateId)
        {
            var rate = await _DbContext.Rates.FindAsync(rateId);
            if (rate == null) return false;
            rate.LikesCount++;
            _DbContext.Rates.Update(rate);
            return await _DbContext.SaveChangesAsync()>0;
        }

        public async Task<bool> DecreamentRateLikeAsync(Guid rateId)
        {
            var rate = await _DbContext.Rates.FindAsync(rateId);
            if (rate == null) return false;
            rate.LikesCount--;
            _DbContext.Rates.Update(rate);
            return await _DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> IncreamentRateDisLikeAsync(Guid rateId)
        {
            var rate = await _DbContext.Rates.FindAsync(rateId);
            if (rate == null) return false;
            rate.DislikesCount++;
            _DbContext.Rates.Update(rate);
            return await _DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DecreamentRateDisLikeAsync(Guid rateId)
        {
            var rate = await _DbContext.Rates.FindAsync(rateId);
            if (rate == null) return false;
            rate.DislikesCount--;
            _DbContext.Rates.Update(rate);
            return await _DbContext.SaveChangesAsync() > 0;
        }

        #endregion

    }
}
