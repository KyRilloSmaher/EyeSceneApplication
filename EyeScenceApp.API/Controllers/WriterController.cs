using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.IServices;
using Microsoft.AspNetCore.Mvc;
using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Application.DTOs.Crew.Writers;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class WriterController : ControllerBase
    {
        private readonly IWriterService _WriterService;
        public WriterController(IWriterService WriterService)
        {
            _WriterService = WriterService;
        }

        [HttpGet(Router.Writer.GetById)]
        public async Task<IActionResult> GetWriterByIdAsync([FromRoute] Guid Id)
        {

            var response = await _WriterService.GetWriterByIdAsync(Id);
            return Router.FinalResponse<WriterDTO>(response);
        }
        [HttpGet(Router.Writer.GetAll)]
        public async Task<IActionResult> GetAllWritersAsync()
        {

            var response = await _WriterService.GetAllWritersAsync();
            return Router.FinalResponse<ICollection<WriterDTO>>(response);
        }
        [HttpGet(Router.Writer.FilterWriters)]
        public async Task<IActionResult> FilterWriters([FromQuery] FilterDTO dto)
        {

            var response = await _WriterService.FilterWriterAsync(dto);
            return Router.FinalResponse<ICollection<WriterDTO>>(response);
        }
        [HttpGet(Router.Writer.GetWritersAwards)]
        public async Task<IActionResult> GetWriterAwards([FromRoute] Guid Id)
        {

            var response = await _WriterService.GetWriterAwardsAsync(Id);
            return Router.FinalResponse<ICollection<CelebirtyAwardDTO>>(response);
        }
        [HttpGet(Router.Writer.GetWritersWorks)]
        public async Task<IActionResult> GetWriterWorks([FromRoute] Guid Id)
        {

            var response = await _WriterService.GetWriterWorksAsync(Id);
            return Router.FinalResponse<ICollection<DigitalContentDTO>>(response);
        }
        [HttpPost(Router.Writer.CreateWriter)]
        public async Task<IActionResult> CreateWriterAsync([FromBody] CreateWriterDTO dto)
        {

            var response = await _WriterService.CreateWriterAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
        [HttpPut(Router.Writer.UpdateWriter)]
        public async Task<IActionResult> UpdateWriterAsync([FromBody] UpdateWriterDTO dto)
        {

            var response = await _WriterService.UpdateWriterAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
        [HttpDelete(Router.Writer.DeleteWriter)]
        public async Task<IActionResult> DeleteWriterAsync([FromRoute] Guid Id)
        {

            var response = await _WriterService.DeleteWriterAsync(Id);
            return Router.FinalResponse<bool>(response);
        }
    }
}
