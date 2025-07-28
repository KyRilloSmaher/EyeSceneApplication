using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.DTOs.WatchList;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace EyeScenceApp.API.Controllers
{
    public class WatchListController : Controller
    {
       private readonly IWatchListService _watchListService;

        public WatchListController(IWatchListService watchListService)
        {
            _watchListService = watchListService;
        }

        [HttpGet(Router.WatchList.GetAllForUser)]
        public async Task<IActionResult> GetAllWatchListForUserAsync(string Id)
        {
            var response = await _watchListService.GetFavoritesOfUserAsync(Id);
            return Router.FinalResponse<ICollection<DigitalContentDTO>>(response);
        }
        [HttpGet(Router.WatchList.IsInWatchList)]
        public async Task<IActionResult> IsInWatchListAsync([FromQuery] IsDigitalContentInUserWatchListDTO dto)
        {
            var response = await _watchListService.IsInWatchListAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
        [HttpPost(Router.WatchList.AddToWatchList)]
        public async Task<IActionResult> AddToWatchListAsync([FromBody] AddDigitalContentToUserWatchListDTO dto)
        {
            var response = await _watchListService.AddToWatchListAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
        [HttpDelete(Router.WatchList.DeleteFromWatchList)]
        public async Task<IActionResult> DeleteFromWatchListAsync([FromBody] DeleteDigitalContentFromUserWatchListDTO dto)
        {
            var response = await _watchListService.DeleteFromWatchListAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
       
        [HttpDelete(Router.WatchList.ClearWatchList)]
        public async Task<IActionResult> ClearWatchListAsync([FromRoute]string Id)
        {
            var response = await _watchListService.ClearWatchListAsync(Id);
            return Router.FinalResponse<bool>(response);
        }


    }
}
