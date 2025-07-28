using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface IWatchListRepository : IGenericRepository<WatchList>
    {
        Task<IEnumerable<WatchList>> GetWatchListByUserIdAsync(string userId);
        Task<bool> IsInWatchListAsync(string userId, Guid DigitalContentId);

        Task<bool> ClearWatchListAsync(string userId);
    }
}
