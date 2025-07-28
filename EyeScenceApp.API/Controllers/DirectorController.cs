using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EyeScenceApp.Application.DTOs.Crew.Directors;
using EyeScenceApp.Application.DTOs.Crew.Base;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _directorService;
        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        [HttpGet(Router.Director.GetById)]
        public async Task<IActionResult> GetDirectorByIdAsync([FromRoute] Guid Id)
        {

            var response = await _directorService.GetDirectorByIdAsync(Id);
            return Router.FinalResponse<DirectorDTO>(response);
        }
        [HttpGet(Router.Director.GetAll)]
        public async Task<IActionResult> GetAllDirectorsAsync()
        {

            var response = await _directorService.GetAllDirectorsAsync();
            return Router.FinalResponse<ICollection<DirectorDTO>>(response);
        }
        [HttpGet(Router.Director.FilterDirectors)]
        public async Task<IActionResult> FilterDirectors([FromQuery] FilterDTO dto)
        {

            var response = await _directorService.FilterDirectorAsync(dto);
            return Router.FinalResponse<ICollection<DirectorDTO>>(response);
        }
        [HttpGet(Router.Director.GetDirectorAwards)]
        public async Task<IActionResult> GetDirectorAwards([FromRoute] Guid Id)
        {

            var response = await _directorService.GetDirectorAwardsAsync(Id);
            return Router.FinalResponse<ICollection<CelebirtyAwardDTO>>(response);
        }
        [HttpGet(Router.Director.GetDirectorWorks)]
        public async Task<IActionResult> GetDirectorWorks([FromRoute] Guid Id)
        {

            var response = await _directorService.GetDirectorWorksAsync(Id);
            return Router.FinalResponse<ICollection<DigitalContentDTO>>(response);

        }
        [HttpPost(Router.Director.CreateDirector)]
        public async Task<IActionResult> CreateDirectorAsync([FromBody] CreateDirectorDTO dto)
        {

            var response = await _directorService.CreateDirectorAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
        [HttpPut(Router.Director.UpdateDirector)]
        public async Task<IActionResult> UpdateDirectorAsync([FromBody] UpdateDirectorDTO dto)
        {

            var response = await _directorService.UpdateDirectorAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
        [HttpDelete(Router.Director.DeleteDirector)]
        public async Task<IActionResult> DeleteDirectorAsync([FromRoute] Guid Id)
        {

            var response = await _directorService.DeleteDirectorAsync(Id);
            return Router.FinalResponse<bool>(response);
        }
    }
}
