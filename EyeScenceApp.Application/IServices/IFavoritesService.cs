using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.DTOs.Favorites;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface IFavoritesService
    {
        Task<Response<bool>> AddFavoriteAsync(AddDigitalContentToUserFavoriteListDTO dto);
        Task<Response<bool>> DeleteFavoriteAsync(DeleteDigitalContentToUserFavoriteListDTO dto);
        Task<Response<ICollection<DigitalContentDTO>>> GetFavoritesOfUserAsync(string userId);
        Task<Response<bool>> IsFavoriteAsync(IsDigitalContentToUserFavoriteListDTO dto);
        Task<Response<bool>> ClearFavoritesAsync(string userId);
    }
}
