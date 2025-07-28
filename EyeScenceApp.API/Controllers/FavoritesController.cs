using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.DTOs.Favorites;
using EyeScenceApp.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoritesService _favoritesService;
        public FavoritesController(IFavoritesService favoritesService)
        {
            _favoritesService = favoritesService;
        }

        [HttpGet(Router.Favorites.GetAllFavoritesOfUser)]
        public async Task<IActionResult> GetFavoritesOfUserAsync([FromRoute] string Id)
        {
            var response = await _favoritesService.GetFavoritesOfUserAsync(Id);
            return Router.FinalResponse<ICollection<DigitalContentDTO>>(response);
        }
        [HttpGet(Router.Favorites.IsExistinFavorites)]
        public async Task<IActionResult> IsFavoriteAsync([FromQuery] IsDigitalContentToUserFavoriteListDTO dto)
        {
            var response = await _favoritesService.IsFavoriteAsync(dto);
            return Router.FinalResponse<bool>(response);
        }

        [HttpPost(Router.Favorites.AddToFavoritesList)]
        public async Task<IActionResult> AddToFavoritesListAsync([FromBody] AddDigitalContentToUserFavoriteListDTO dto)
        {
            var response = await _favoritesService.AddFavoriteAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
        [HttpDelete(Router.Favorites.DeleteFromFavoritesList)]
        public async Task<IActionResult> DeleteFromFavoritesListAsync([FromBody] DeleteDigitalContentToUserFavoriteListDTO dto)
        {
            var response = await _favoritesService.DeleteFavoriteAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
     
        [HttpDelete(Router.Favorites.ClearFavorites)]
        public async Task<IActionResult> ClearFavoritesAsync([FromRoute] string Id)
        {
            var response = await _favoritesService.ClearFavoritesAsync(Id);
            return Router.FinalResponse<bool>(response);
        }
    }
}
