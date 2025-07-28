using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using EyeScenceApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EyeScenceApp.Infrastructure.Repositories
{
    public class EpisodeRepository : GenericRepository<Episode>,IEpisodeRepository
    {
        #region Field(s)
        private readonly ApplicationDbContext _dbcontext;
        #endregion

        #region Constructor(s)
        public EpisodeRepository(ApplicationDbContext context) : base(context)
        {
            _dbcontext = context ?? throw new ArgumentNullException(nameof(context), "ApplicationDbContext cannot be null.");
        }
        public override async Task<Episode> AddAsync(Episode episode)
        {
            var addedEpisode = await _dbContext.Episodes.AddAsync(episode);
            if (addedEpisode is not null)
            {
                var series = await _dbContext.Series.FirstOrDefaultAsync(series => series.Id == episode.SeriesId);
                if (series is not null)
                {
                    series.EpisodesCount++;
                    series.DurationByMinutes = await _dbContext.Episodes.Where(eps =>eps.SeriesId == series.Id).SumAsync(eps=>eps.DurationByMinutes);
                    await _dbContext.SaveChangesAsync();
                    return episode;
                }
            }
            return null;
        }

        public override  async Task DeleteAsync(Episode episode)
        {
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                _dbContext.Episodes.Remove(episode);
                var series =await _dbContext.Series.FirstOrDefaultAsync(series => series.Id == episode.SeriesId);
                if (series is null)
                    throw new NullReferenceException("Series Of Episode Not Found");
                series.EpisodesCount--;
                series.DurationByMinutes -= episode.DurationByMinutes;
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.CommitTransactionAsync();
            }
            catch(Exception ex)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                throw new Exception("");
            }
         
        }

        public async Task<IEnumerable<Episode>> GetEpsidoesOfSeriesAsync(Guid seriesId)
        {
            return await _dbcontext.Episodes.Where(e => e.SeriesId == seriesId)
                .Include(series=>series.poster)
                .ToListAsync();
        }

        public async Task<IEnumerable<Episode>> GetEpsidoesInSeasonOfSeriesAsync(Guid seriesId, int seasonNumber)
        {
            return await _dbcontext.Episodes.Where(e => e.SeriesId == seriesId && e.Season == seasonNumber).ToListAsync();
        }

        #endregion
    }
}
