using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        Task<bool> IsFavoriteAsync(string userId, Guid DigitalContentId);
        Task<IEnumerable<Favorite>> GetFavoritesByUserIdAsync(string userId);
        Task<bool> ClearFavoritesAsync(string userId);
    }
}
