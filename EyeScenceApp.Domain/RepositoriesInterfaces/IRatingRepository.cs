using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface IRatingRepository : IGenericRepository<Rate> 
    {
        public Task<IEnumerable<Rate>> GetAllRatesByUserAsync(string UserId , DigitalContent? digitalContentType = null);
        public Task<IEnumerable<DigitalContent>> GetTopRatedDigitalContentsAsync(int topCount = 10);
        public Task<IEnumerable<DigitalContent>> GetTopRatedDigitalContentsByGenreAsync(string genreName, int topCount = 10);
        public Task<IEnumerable<Rate>> GetAllRatesOfDigitalContentAsync(Guid id);
        public double GetRatesOfDigitalContentAsync(Guid DigitalContentid);
        public Task<bool> IncreamentRateLikeAsync(Guid rateId);
        public Task<bool> DecreamentRateLikeAsync(Guid rateId);
        public Task<bool> IncreamentRateDisLikeAsync(Guid rateId);
        public Task<bool> DecreamentRateDisLikeAsync(Guid rateId);


    }
}
