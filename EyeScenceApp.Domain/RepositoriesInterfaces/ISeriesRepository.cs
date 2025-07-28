using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface ISeriesRepository : IGenericRepository<Series>
    {
        public Task<IEnumerable<Series?>> GetSeriesByTitleAsync(string title);
        public Task<IEnumerable<Series?>> GetSeriesByGenreIdAsync(byte genreId);
        public Task<IEnumerable<Series?>> GetNewAddedSeriesAsync(byte genreId = 0);
        public Task<IEnumerable<Series?>> GetTopRatedSeriesAsync(int topCount = 10);
    }
}
