using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface IEpisodeRepository : IGenericRepository<Episode>
    {
        public Task<IEnumerable<Episode>> GetEpsidoesOfSeriesAsync(Guid seriesId);
        public Task<IEnumerable<Episode>> GetEpsidoesInSeasonOfSeriesAsync(Guid seriesId, int seasonNumber);
    }
}
