using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.DTOs.Favorites;
using EyeScenceApp.Application.DTOs.WatchList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface IWatchListService
    {
        Task<Response<bool>> AddToWatchListAsync(AddDigitalContentToUserWatchListDTO dto);
        Task<Response<bool>> DeleteFromWatchListAsync(DeleteDigitalContentFromUserWatchListDTO dto);
        Task<Response<ICollection<DigitalContentDTO>>> GetFavoritesOfUserAsync(string userId);
        Task<Response<bool>> IsInWatchListAsync(IsDigitalContentInUserWatchListDTO dto);
        Task<Response<bool>> ClearWatchListAsync(string userId);
    }
}
