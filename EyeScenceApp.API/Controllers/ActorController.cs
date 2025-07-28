using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;
        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet(Router.Actor.GetById)]
        public async Task<IActionResult> GetActorByIdAsync([FromRoute]Guid Id)
        {
         
            var response = await _actorService.GetActorByIdAsync(Id);
            return Router.FinalResponse<ActorDTO>(response);
        }
        [HttpGet(Router.Actor.GetAll)]
        public async Task<IActionResult> GetAllActorsAsync()
        {

            var response = await _actorService.GetAllActorsAsync();
            return Router.FinalResponse<ICollection<ActorDTO>>(response);
        }
        [HttpGet(Router.Actor.SearchActorsByName)]
        public async Task<IActionResult> GetActorByNameAsync([FromRoute]string name)
        {

            var response = await _actorService.SearchActorAsync(name);
            return Router.FinalResponse<ICollection<ActorDTO>>(response);
        }
        [HttpGet(Router.Actor.FilterActors)]
        public async Task<IActionResult> FilterActors([FromQuery] ActorFilterDTO dto)
        {

            var response = await _actorService.FilterActorAsync(dto);
            return Router.FinalResponse<ICollection<ActorDTO>>(response);
        }
        [HttpGet(Router.Actor.GetActorAwards)]
        public async Task<IActionResult> GetActorAwards([FromRoute] Guid Id)
        {

            var response = await _actorService.GetActorAwardsAsync(Id);
            return Router.FinalResponse<ICollection<CelebirtyAwardDTO>>(response);
        }
        [HttpGet(Router.Actor.GetActorWorks)]
        public async Task<IActionResult> GetActorWorks([FromRoute] Guid Id)
        {

            var response = await _actorService.GetActorWorksAsync(Id);
            return Router.FinalResponse<ICollection<DigitalContentDTO>>(response);
        }
        [HttpPost(Router.Actor.CreateActor)]
        public async Task<IActionResult> CreateActorAsync([FromForm] CreateActorDTO actorDto)
        {
            var response = await _actorService.CreateActorAsync(actorDto);
            return Router.FinalResponse<bool>(response);
        }

        [HttpPut(Router.Actor.UpdateActor)]
        public async Task<IActionResult> UpdateActorAsync([FromForm] UpdateActorDTO actorDto)
        {
            var response = await _actorService.UpdateActorAsync(actorDto);
            return Router.FinalResponse<bool>(response);
        }

        [HttpDelete(Router.Actor.DeleteActor)]
        public async Task<IActionResult> DeleteActorAsync(Guid Id)
        {
            var response = await _actorService.DeleteActorAsync(Id);
            return Router.FinalResponse<bool>(response);
        }
    }
}
