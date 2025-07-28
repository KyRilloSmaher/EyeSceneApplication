using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using EyeScenceApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Infrastructure.Repositories
{
    public class AwardRepository : GenericRepository<Award>, IAwardRepository
    {
        #region Field(s)
        private readonly ApplicationDbContext _dbContext;
    
        #endregion

        #region Constructor(s)
        public AwardRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext ?? throw new ArgumentNullException(nameof(_dbContext), "ApplicationDbContext cannot be null.");

        }

        #endregion

        #region Method(s)

        public async override Task<IEnumerable<Award>> GetAllAsync(bool isTracked = false)
        {
            var query = _dbContext.Awards.Include(award => award.Image);

            return isTracked
                ? await query.ToListAsync()
                : await query.AsNoTracking().ToListAsync();
        }

            public async  Task<IEnumerable<DigitalContentAward>> GetAllDigitalContentAwardsAsync()
            {

                return await _dbContext.DigitalContentAwards.Include(award => award.Image).ToListAsync();
            }
            public async  Task<IEnumerable<CelebirtyAward>> GetAllCelebirtyAwardsAsync()
            {
                return await _dbContext.CelebirtyAwards.Include(award => award.Image).ToListAsync();
            }
            public async Task<Award?> GetAwardByNameAsync(string name, bool isTracked = false)
            {
                return isTracked
                     ? await _dbContext.Awards.Include(award => award.Image).FirstOrDefaultAsync(a => a.Name == name)
                    : await _dbContext.Awards.Include(award => award.Image).AsNoTracking().FirstOrDefaultAsync(a => a.Name == name);
            }
            public async Task<IEnumerable<Award>> GetAwardsByCategoryAsync(string category, bool isTracked = false)
            {
                var query = _dbContext.Awards.Include(award => award.Image)
                                    .Where(a => a.Category.Contains(category));

                return isTracked
                    ? await query.ToListAsync()
                    : await query.AsNoTracking().ToListAsync();
            }
            public async Task<IEnumerable<CelebirtyAward>> GetAwardsByCelebrityAsync(Guid celebrityId, bool isTracked = false)
            {
                if (celebrityId == Guid.Empty)
                {
                    throw new ArgumentException("Celebrity ID cannot be empty.", nameof(celebrityId));
                }
                var celebrityExist = await _dbContext.Celebrities.FindAsync(celebrityId);

                if (celebrityExist is null)
                {
                    throw new KeyNotFoundException($"Celebrity with ID {celebrityId} not found.");
                }
                var query = _dbContext.CelebirtyAwardsJoin.Include(ca => ca.Award).ThenInclude(award=>award.Image)
                                      .Where(ca => ca.Celebirtyid == celebrityId)
                                      .Select(ca => ca.Award);

                return isTracked
                    ? await query.ToListAsync()
                    : await query.AsNoTracking().ToListAsync();
            }
            public async Task<IEnumerable<DigitalContentAward>> GetAwardsByDigitalContentAsync(Guid digitalContentId, bool isTracked = false)
        {
           
            var DigitalContentExist = await _dbContext.DigitalContents.FindAsync(digitalContentId);

            if (DigitalContentExist is null)
            {
                throw new KeyNotFoundException($"Celebrity with ID {digitalContentId} not found.");
            }
            var query = _dbContext.DigitalContentAwardsJoin.Include(ca => ca.Award).ThenInclude(award => award.Image)
                                  .Where(ca => ca.DigitalContentId == digitalContentId)
                                  .Select(ca => ca.Award);

            return isTracked
                ? await query.ToListAsync()
                : await query.AsNoTracking().ToListAsync();
        }
            public async Task<IEnumerable<Award>> GetAwardsByOrganizationAsync(string organizationName, bool isTracked = false)
            {
                var query = _dbContext.Awards.Include(award => award.Image)
                                      .Where(a => a.Organization.Contains(organizationName));

                return isTracked
                    ? await query.ToListAsync()
                    : await query.AsNoTracking().ToListAsync();
            }
            public async Task<DigitalContentAward ?> GetDigitalContentAwardByIdAsync (Guid id, bool isTracked = false)
            {
                var query = _dbContext.DigitalContentAwards.Include(dc => dc.Image);

                return isTracked
                    ? await query.FirstOrDefaultAsync(dc => dc.Id == id)
                    : await query.AsNoTracking().FirstOrDefaultAsync(dc => dc.Id == id);
            }

            public async Task<CelebirtyAward?> GetCelebirtyAwardByIdAsync(Guid id, bool isTracked = false)
            {
                var query = _dbContext.CelebirtyAwards.Include(dc => dc.Image);

                return isTracked
                    ? await query.FirstOrDefaultAsync(dc => dc.Id == id)
                    : await query.AsNoTracking().FirstOrDefaultAsync(dc => dc.Id == id);
            }

            public async Task<bool> AddAwardToCelebirtyAsync(Award award, Celebrity celebirty, int year)
            {
                if (award == null) throw new ArgumentNullException(nameof(award));
                if (celebirty == null) throw new ArgumentNullException(nameof(celebirty));

                var celebirtyAwardsJoin = new CelebirtyAwards
                {
                    AwardId = award.Id,
                    Celebirtyid = celebirty.Id,
                    Year = year
                };

                _dbContext.CelebirtyAwardsJoin.Add(celebirtyAwardsJoin);
                await _dbContext.SaveChangesAsync();
                return true;

            }

            public async Task<bool> AddAwardToDigitalContentAsync(Award award, DigitalContent digitalContent, int Year)
            {
                if (award == null) throw new ArgumentNullException(nameof(award));
                if (digitalContent == null) throw new ArgumentNullException(nameof(digitalContent));

                var digitalContentAward = new DigitalContentAwards
                {
                    AwardId = award.Id,
                    DigitalContentId = digitalContent.Id,
                    Year = Year
                };

                _dbContext.DigitalContentAwardsJoin.Add(digitalContentAward);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            public async Task<CelebirtyAward> CreateCelebirtyAwardAsync(CelebirtyAward award)
            {
                if (award == null) throw new ArgumentNullException(nameof(award));

                _dbContext.CelebirtyAwards.Add(award);
                await _dbContext.SaveChangesAsync();
                return award;
            }

            public async Task<DigitalContentAward> CreateDigitalContentAwardAsync(DigitalContentAward award)
            {
                if (award == null) throw new ArgumentNullException(nameof(award));

                _dbContext.DigitalContentAwards.Add(award);
                await _dbContext.SaveChangesAsync();
                return award;
            }

            public async Task<bool> RemoveAwardFromCelebrityAsync(Guid awardId, Guid celebirtyId, int Year)
            {
                var query = _dbContext.CelebirtyAwardsJoin
                    .Where(ca => ca.AwardId == awardId && ca.Celebirtyid == celebirtyId && ca.Year == Year).Include(award => award.Award).ThenInclude(a => a.Image)
                    .FirstOrDefault();
                if (query == null)
                {
                    throw new KeyNotFoundException($"Award with ID {awardId} for Celebrity with ID {celebirtyId} not found for the year {Year}.");
                }
                _dbContext.CelebirtyAwardsJoin.Remove(query);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            public async Task<bool> RemoveAwardFromDigitalContentAsync(Guid awardId, Guid celdigitalContentIdebirtyId, int Year)
            {
                var query = _dbContext.DigitalContentAwardsJoin
                    .Where(dc => dc.AwardId == awardId && dc.DigitalContentId == celdigitalContentIdebirtyId && dc.Year == Year).Include(award => award.Award).ThenInclude(a => a.Image)
                    .FirstOrDefault();
                if (query == null)
                {
                    throw new KeyNotFoundException($"Award with ID {awardId} for Digital Content with ID {celdigitalContentIdebirtyId} not found for the year {Year}.");
                }
                _dbContext.DigitalContentAwardsJoin.Remove(query);
                await _dbContext.SaveChangesAsync();
                return true;
            }

     
        #endregion
    }
}
