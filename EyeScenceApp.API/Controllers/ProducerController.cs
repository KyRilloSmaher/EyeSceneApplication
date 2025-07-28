using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EyeScenceApp.Application.DTOs.Crew.Producers;
using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Application.Services;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IProducerService _ProducerService;
        public ProducerController(IProducerService ProducerService)
        {
            _ProducerService = ProducerService;
        }

        [HttpGet(Router.Producer.GetById)]
        public async Task<IActionResult> GetProducerByIdAsync([FromRoute] Guid Id)
        {

            var response = await _ProducerService.GetProducerByIdAsync(Id);
            return Router.FinalResponse<ProducerDTO>(response);
        }
        [HttpGet(Router.Producer.GetAll)]
        public async Task<IActionResult> GetAllProducersAsync()
        {

            var response = await _ProducerService.GetAllProducersAsync();
            return Router.FinalResponse<ICollection<ProducerDTO>>(response);
        }
        [HttpGet(Router.Producer.FilterProducers)]
        public async Task<IActionResult> FilterProducers([FromQuery] FilterDTO dto)
        {

            var response = await _ProducerService.FilterProducerAsync(dto);
            return Router.FinalResponse<ICollection<ProducerDTO>>(response);
        }
        [HttpGet(Router.Producer.GetProducerAwards)]
        public async Task<IActionResult> GetProducerAwards([FromRoute] Guid Id)
        {

            var response = await _ProducerService.GetProducerAwardsAsync(Id);
            return Router.FinalResponse<ICollection<CelebirtyAwardDTO>>(response);
        }
        [HttpGet(Router.Producer.GetProducerWorks)]
        public async Task<IActionResult> GetProducerWorks([FromRoute] Guid Id)
        {

            var response = await _ProducerService.GetProducerWorksAsync(Id);
            return Router.FinalResponse<ICollection<DigitalContentDTO>>(response);
        }
        [HttpPost(Router.Producer.CreateProducer)]
        public async Task<IActionResult> CreateProducerAsync([FromBody] CreateProducerDTO dto)
        {

            var response = await _ProducerService.CreateProducerAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
        [HttpPut(Router.Producer.UpdateProducer)]
        public async Task<IActionResult> UpdateProducerAsync([FromBody] UpdateProducerDTO dto)
        {

            var response = await _ProducerService.UpdateProducerAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
        [HttpDelete(Router.Producer.DeleteProducer)]
        public async Task<IActionResult> DeleteProducerAsync([FromRoute] Guid Id)
        {

            var response = await _ProducerService.DeleteProducerAsync(Id);
            return Router.FinalResponse<bool>(response);
        }
    }
}
