using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.Awards;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.Awards.DigitalContentAwards;
using EyeScenceApp.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class AwardController : ControllerBase
    {
        private readonly IAwardService _awardService;
        public AwardController(IAwardService awardService)
        {
            _awardService = awardService ?? throw new ArgumentNullException(nameof(awardService), "Award service cannot be null.");
        }


        [HttpGet(Router.Award.GetAll)]
        public async Task<IActionResult> GetAllAwardsAsync()
        {
            var response = await _awardService.GetAllAwardsAsync();
            return Router.FinalResponse<IEnumerable<AwardDTO>>(response);
        }
        [HttpGet(Router.Award.GetAwardByName)]
        public async Task<IActionResult> GetAwardByNameAsync(string name)
        {
            var response = await _awardService.GetAwardByNameAsync(name);
            return Router.FinalResponse<AwardDTO>(response);
        }

        [HttpGet(Router.Award.GetAwardsByOrganization)]
        public async Task<IActionResult> GetAwardsByOrganizationAsync(string organizationName)
        {
            var response = await _awardService.GetAwardsByOrganizationAsync(organizationName);
            return Router.FinalResponse<IEnumerable<AwardDTO>>(response);
        }
        [HttpGet(Router.Award.GetAwardsByCategory)]
        public async Task<IActionResult> GetAwardsByCategoryAsync(string category)
        {
            var response = await _awardService.GetAwardsByCategoryAsync(category);
            return Router.FinalResponse<IEnumerable<AwardDTO>>(response);
        }

        [HttpGet(Router.Award.GetDigitalContentAwardById)]
        public async Task<IActionResult> GetDigitalContentAwardByIdAsync(Guid Id)
        {
            var response = await _awardService.GetDigitalContentAwardByIdAsync(Id);
            return Router.FinalResponse<DigitalContentAwardDTO>(response);
        }
        [HttpGet(Router.Award.GetAllDigitalContentAwards)]
        public async Task<IActionResult> GetAllDigitalContentAwardsAsync()
        {
            var response = await _awardService.GetAllDigitalContentAwardsAsync();
            return Router.FinalResponse<IEnumerable<DigitalContentAwardDTO>>(response);
        }
        [HttpGet(Router.Award.GetDigitalContentAwardsByContent)]
        public async Task<IActionResult> GetDigitalContentAwardsByDigitalContentAsync(Guid digitalContentId)
        {
            var response = await _awardService.GetDigitalContentAwardsByDigitalContentAsync(digitalContentId);
            return Router.FinalResponse<IEnumerable<DigitalContentAwardDTO>>(response);
        }
      
        [HttpGet(Router.Award.GetCelebirtyAwardById)]
        public async Task<IActionResult> GetCelebirtyAwardByIdAsync(Guid Id)
        {
            var response = await _awardService.GetCelebirtyAwardByIdAsync(Id);
            return Router.FinalResponse<CelebirtyAwardDTO>(response);
        }
        [HttpGet(Router.Award.GetAllCelebirtyAwards)]
        public async Task<IActionResult> GetAllCelebirtyAwardsAsync()
        {
            var response = await _awardService.GetAllCelebirtyAwardsAsync();
            return Router.FinalResponse<IEnumerable<CelebirtyAwardDTO>>(response);
        }
        [HttpGet(Router.Award.GetAwardsByCelebrity)]
        public async Task<IActionResult> GetAwardsByCelebrityAsync(Guid celebrityId)
        {
            var response = await _awardService.GetAwardsByCelebrityAsync(celebrityId);
            return Router.FinalResponse<IEnumerable<CelebirtyAwardDTO>>(response);
        }
        [HttpPost(Router.Award.CreateCelebirtyAward)]
        public async Task<IActionResult> CreateCelebirtyAwardAsync([FromForm] CreateCelebirtyAwardDTO awardDto)
        {
            var response = await _awardService.CreateCelebirtyAwardAsync(awardDto);
            return Router.FinalResponse<CelebirtyAwardDTO>(response);
        }
        [HttpPost(Router.Award.AddAwardToCelebrity)]
        public async Task<IActionResult> AddAwardToCelebirtyAsync(Guid awardId, Guid celebirtyId, int year)
        {
            var response = await _awardService.AddAwardToCelebirtyAsync(awardId, celebirtyId, year);
            return Router.FinalResponse<bool>(response);
        }
        [HttpPost(Router.Award.CreateDigitalContentAward)]
        public async Task<IActionResult> CreateDigitalContentAwardAsync([FromForm] CreateDigitalContentAwardDTO awardDTO)
        {
            var response = await _awardService.CreateDigitalContentAwardAsync(awardDTO);
            return Router.FinalResponse<DigitalContentAwardDTO>(response);
        }
        [HttpPost(Router.Award.AddAwardToDigitalContent)]
        public async Task<IActionResult> AddAwardToDigitalContentAsync(Guid awardId, Guid digitalContentId, int year)
        {
            var response = await _awardService.AddAwardToDigitalContentAsync(awardId, digitalContentId, year);
            return Router.FinalResponse<bool>(response);
        }
        
        [HttpPut(Router.Award.UpdateCelebirtyAward)]
        public async Task<IActionResult> UpdateCelebirtyAwardAsync([FromForm] UpdateCelebirtyAwardDTO awardDTO)
        {
            var response = await _awardService.UpdateCelebirtyAwardAsync(awardDTO);
            return Router.FinalResponse<CelebirtyAwardDTO>(response);
        }

        [HttpPut(Router.Award.UpdateDigitalContentAward)]
        public async Task<IActionResult> UpdateDigitalContentAwardAsync([FromForm] UpdateDigitalContentAwardDTO awardDTO)
        {
            var response = await _awardService.UpdateDigitalContentAwardAsync(awardDTO);
            return Router.FinalResponse<DigitalContentAwardDTO>(response);
        }

        [HttpDelete(Router.Award.DeleteCelebirtyAward)]
        public async Task<IActionResult> DeleteCelebirtyAwardAsync(Guid awardId)
        {
            var response = await _awardService.DeleteCelebirtyAwardAsync(awardId);
            return Router.FinalResponse<bool>(response);

        }
        [HttpDelete(Router.Award.RemoveAwardFromCelebrity)]
        public async Task<IActionResult> RemoveAwardFromCelebrityAsync(Guid awardId, Guid celebirtyId, int year)
        {
            var response = await _awardService.RemoveAwardFromCelebrityAsync(awardId, celebirtyId, year);
            return Router.FinalResponse<bool>(response);
        }
        [HttpDelete(Router.Award.DeleteDigitalContentAward)]
        public async Task<IActionResult> DeleteDigitalContentAwardAsync(Guid awardId)
        {
            var response = await _awardService.DeleteDigitalContentAwardAsync(awardId);
            return Router.FinalResponse<bool>(response);
        }
        [HttpDelete(Router.Award.RemoveAwardFromDigitalContent)]
        public async Task<IActionResult> RemoveAwardFromDigitalContentAsync(Guid awardId, Guid digitalContentId, int year)
        {
            var response = await _awardService.RemoveAwardFromDigitalContentAsync(awardId, digitalContentId, year);
            return Router.FinalResponse<bool>(response);
        }
    }

}
