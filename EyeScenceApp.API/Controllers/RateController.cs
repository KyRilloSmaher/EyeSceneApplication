using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.Rates;
using EyeScenceApp.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;
        public RateController(IRateService rateService)
        {
            _rateService = rateService;

        }
        [HttpGet(Router.Rates.GetRateById)]
        public async Task<IActionResult> GetRateById([FromRoute] Guid Id)
        {
            var response = await _rateService.GetRateByIdAsync(Id);
            return Router.FinalResponse(response);
        }
        [HttpGet(Router.Rates.GetAllForUser)]
        public async Task<IActionResult> GetAllForUser([FromRoute] string Id)
        {
            var response = await _rateService.GetAllRatesByUserAsync(Id);
            return Router.FinalResponse(response);
        }
        [HttpGet(Router.Rates.GetRateByDigitalContentId)]
        public async Task<IActionResult> GetRateByDigitalContentId([FromRoute] Guid Id)
        {
            var response = await _rateService.GetAllRatesOfDigitalContentAsync(Id);
            return Router.FinalResponse(response);
        }
        [HttpGet(Router.Rates.GetTopRatedDigitalContent)]
        public async Task<IActionResult> GetTopRatedDigitalContent()
        {
            var response = await _rateService.GetTopRatedDigitalContentAsync();
            return Router.FinalResponse(response);
        }
        [HttpGet(Router.Rates.GetTopRatedDigitalContentInGenre)]
        public async Task<IActionResult> GetTopRatedDigitalContentByGenreName([FromRoute] string name)
        {
            var response = await _rateService.GetTopRatedDigitalContentByGenreNameAsync(name);
            return Router.FinalResponse(response);
        }
        [HttpPost(Router.Rates.CreateRate)]
        public async Task<IActionResult> CreateRate([FromBody] CreateRateDTO rateDto)
        {
            var response = await _rateService.CreateRateAsync(rateDto);
            return Router.FinalResponse(response);
        }
        [HttpPut(Router.Rates.UpdateRate)]
        public async Task<IActionResult> UpdateRate([FromBody] UpdateRateDTO rateDto)
        {
            var response = await _rateService.UpdateRateAsync(rateDto);
            return Router.FinalResponse(response);
        }

        [HttpPut(Router.Rates.LikeRate)]
        public async Task<IActionResult> LikeRate([FromRoute] Guid Id)
        {
            var response = await _rateService.IncreamentRateLikeAsync(Id);
            return Router.FinalResponse(response);
        }
        [HttpPut(Router.Rates.RemoveLikeRate)]
        public async Task<IActionResult> RemoveLikeRate([FromRoute] Guid Id)
        {
            var response = await _rateService.DecreamentRateLikeAsync(Id);
            return Router.FinalResponse(response);
        }
        [HttpPut(Router.Rates.DisLikeRate)]
        public async Task<IActionResult> DisLikeRate([FromRoute] Guid Id)
        {
            var response = await _rateService.IncreamentRateDisLikeAsync(Id);
            return Router.FinalResponse(response);
        }
        [HttpPut(Router.Rates.RemoveDisLikeRate)]
        public async Task<IActionResult> RemoveDisLikeRate([FromRoute] Guid Id)
        {
            var response = await _rateService.DecreamentRateDisLikeAsync(Id);
            return Router.FinalResponse(response);
        }
        [HttpDelete(Router.Rates.DeleteRate)]
        public async Task<IActionResult> DeleteRate([FromRoute] Guid Id)
        {
            var response = await _rateService.DeleteRateAsync(Id);
            return Router.FinalResponse(response);
        }
    }
}
