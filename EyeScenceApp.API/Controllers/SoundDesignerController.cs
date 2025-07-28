using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EyeScenceApp.Application.DTOs.Crew.SoundDesigners;
using EyeScenceApp.Application.DTOs.Crew.Base;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class SoundDesignerController : ControllerBase
    {
        private readonly ISoundDesignerService _SoundDesignerService;
        public SoundDesignerController(ISoundDesignerService SoundDesignerService)
        {
            _SoundDesignerService = SoundDesignerService;
        }

        [HttpGet(Router.SoundDesigner.GetById)]
        public async Task<IActionResult> GetSoundDesignerByIdAsync([FromRoute] Guid Id)
        {

            var response = await _SoundDesignerService.GetSoundDesignerByIdAsync(Id);
            return Router.FinalResponse<SoundDesignerDTO>(response);
        }
        [HttpGet(Router.SoundDesigner.GetAll)]
        public async Task<IActionResult> GetAllSoundDesignersAsync()
        {

            var response = await _SoundDesignerService.GetAllSoundDesignersAsync();
            return Router.FinalResponse<ICollection<SoundDesignerDTO>>(response);
        }
        [HttpGet(Router.SoundDesigner.FilterSoundDesigners)]
        public async Task<IActionResult> FilterSoundDesigners([FromQuery] FilterDTO dto)
        {

            var response = await _SoundDesignerService.FilterSoundDesignerAsync(dto);
            return Router.FinalResponse<ICollection<SoundDesignerDTO>>(response);
        }
        [HttpGet(Router.SoundDesigner.GetSoundDesignersAwards)]
        public async Task<IActionResult> GetSoundDesignerAwards([FromRoute] Guid Id)
        {

            var response = await _SoundDesignerService.GetSoundDesignerAwardsAsync(Id);
            return Router.FinalResponse<ICollection<CelebirtyAwardDTO>>(response);
        }
        [HttpGet(Router.SoundDesigner.GetSoundDesignersWorks)]
        public async Task<IActionResult> GetSoundDesignerWorks([FromRoute] Guid Id)
        {

            var response = await _SoundDesignerService.GetSoundDesignerWorksAsync(Id);
            return Router.FinalResponse<ICollection<DigitalContentDTO>>(response);
        }
        [HttpPost(Router.SoundDesigner.CreateSoundDesigner)]
        public async Task<IActionResult> CreateSoundDesignerAsync([FromBody] CreateSoundDesignerDTO dto)
        {

            var response = await _SoundDesignerService.CreateSoundDesignerAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
        [HttpPut(Router.SoundDesigner.UpdateSoundDesigner)]
        public async Task<IActionResult> UpdateSoundDesignerAsync([FromBody] UpdateSoundDesignerDTO dto)
        {

            var response = await _SoundDesignerService.UpdateSoundDesignerAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
        [HttpDelete(Router.SoundDesigner.DeleteSoundDesigner)]
        public async Task<IActionResult> DeleteSoundDesignerAsync([FromRoute] Guid Id)
        {

            var response = await _SoundDesignerService.DeleteSoundDesignerAsync(Id);
            return Router.FinalResponse<bool>(response);
        }
    }
}
