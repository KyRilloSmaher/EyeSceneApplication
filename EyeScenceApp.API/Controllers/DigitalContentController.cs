using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.DTOs.SingleDocumentries;
using EyeScenceApp.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class DigitalContentController : ControllerBase
    {
        private readonly IDigitalContentService _digitalContentService;

        public DigitalContentController(IDigitalContentService digitalContentService)
        {
            _digitalContentService = digitalContentService;
        }

        [HttpPost(Router.DigitalContents.AddGenreToDigitalContent)]
        public async Task<IActionResult> AddGenreToDigitalContent([FromRoute] Guid Id, [FromRoute] byte genreId)
        {
            AddGenreToDigitalContentRequest request = new AddGenreToDigitalContentRequest
            {
                DigitalContentId = Id,
                GenreId = genreId
            };
            var response = await _digitalContentService.AddGenreToDigitalContentAsync(request);
            return Router.FinalResponse<bool>(response);
        }
        [HttpPost(Router.DigitalContents.AddActorToDigitalContentCast)]
        public async Task<IActionResult> AddActorToDigitalContentCast([FromRoute] Guid Id, [FromRoute] Guid actorId)
        {
            AddActorToDigitalContentCastRequest request = new AddActorToDigitalContentCastRequest
            {
                DigitalContentId = Id,
                ActorId = actorId
            };
            var response = await _digitalContentService.AddActorToDigitalContentCastAsync(request);
            return Router.FinalResponse<bool>(response);
        }
        [HttpPost(Router.DigitalContents.AddCrewMemberToDigitalContent)]
        public async Task<IActionResult> AddCrewMemberToDigitalContentCast([FromRoute] Guid Id, [FromRoute] Guid crewId)
        {
            AddCrewMemberFromDigitalContentCastRequest request = new AddCrewMemberFromDigitalContentCastRequest
            {
                DigitalContentId = Id,
                CrewId = crewId
            };
            var response = await _digitalContentService.AddCrewMemberToDigitalContentAsync(request);
            return Router.FinalResponse<bool>(response);
        }
        [HttpPost(Router.DigitalContents.AddAwardToDigitalContent)]
        public async Task<IActionResult> AddAwardToDigitalContent([FromBody] AddAwardToDigitalContentRequest request)
        {
            var response = await _digitalContentService.AddAwardToDigitalContentAsync(request);
            return Router.FinalResponse<bool>(response);
        }




        [HttpDelete(Router.DigitalContents.RemoveGenre)]
        public async Task<IActionResult> DeleteGenreFromDigitalContent([FromRoute] Guid Id, [FromRoute] byte genreId)
        {
            DeleteGenreFromDigitalContentRequest request = new DeleteGenreFromDigitalContentRequest
            {
                DigitalContentId = Id,
                GenreId = genreId
            };
            var response = await _digitalContentService.DeleteGenreFromDigitalContentAsync(request);
            return Router.FinalResponse<bool>(response);
        }
        [HttpDelete(Router.DigitalContents.RemoveActor)]
        public async Task<IActionResult> RemoveActorFromDigitalContent([FromRoute] Guid Id, [FromRoute] Guid actorId)
        {
            DeleteActorFromDigitalContentCastRequest request = new DeleteActorFromDigitalContentCastRequest
            {
                DigitalContentId = Id,
                ActorId = actorId
            };
            var response = await _digitalContentService.DeleteActorFromDigitalContentCastAsync(request);
            return Router.FinalResponse<bool>(response);
        }
        [HttpDelete(Router.DigitalContents.RemoveCrewMember)]
        public async Task<IActionResult> RemoveCrewMemberFromDigitalContent([FromRoute] Guid Id, [FromRoute] Guid crewId)
        {
            DeleteCrewMemberFromDigitalContentCastRequest request = new DeleteCrewMemberFromDigitalContentCastRequest
            {
                DigitalContentId = Id,
                CrewId = crewId
            };
            var response = await _digitalContentService.DeleteCrewMemberFromDigitalContentAsync(request);
            return Router.FinalResponse<bool>(response);
        }
        [HttpDelete(Router.DigitalContents.RemoveAward)]
        public async Task<IActionResult> DeleteAwardFromDigitalContent([FromRoute] Guid Id, [FromRoute] Guid awardId)
        {
            DeleteAwardFromDigitalContentRequest request = new DeleteAwardFromDigitalContentRequest
            {
                DigitalContentId = Id,
                AwardId = awardId
            };
            var response = await _digitalContentService.DeleteAwardFromDigitalContentAsync(request);
            return Router.FinalResponse<bool>(response);

        }
    }
}
