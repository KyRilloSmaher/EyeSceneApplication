
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Awards;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.Awards.DigitalContentAwards;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface IAwardService
    {
        public Task<Response<AwardDTO>> GetAwardByNameAsync(string name);
        public Task<Response<IEnumerable<AwardDTO>>> GetAwardsByOrganizationAsync(string organizationName);
        public Task<Response<IEnumerable<AwardDTO>>> GetAwardsByCategoryAsync(string category);
        public Task<Response<IEnumerable<AwardDTO>>> GetAllAwardsAsync();

        public Task<Response<DigitalContentAwardDTO>> GetDigitalContentAwardByIdAsync(Guid awardId);
        public Task<Response<IEnumerable<DigitalContentAwardDTO>>> GetAllDigitalContentAwardsAsync();
        public Task<Response<IEnumerable<DigitalContentAwardDTO>>> GetDigitalContentAwardsByDigitalContentAsync(Guid digitalContentId);
        public Task<Response<DigitalContentAwardDTO>> CreateDigitalContentAwardAsync(CreateDigitalContentAwardDTO awardDTO);
        public Task<Response<bool>> AddAwardToDigitalContentAsync(Guid awardId, Guid digitalContentId,int year);
        public Task<Response<bool>> RemoveAwardFromDigitalContentAsync(Guid awardId, Guid digitalContentId, int Year);
        public Task<Response<DigitalContentAwardDTO>> UpdateDigitalContentAwardAsync(UpdateDigitalContentAwardDTO awardDTO);
        public Task<Response<bool>> DeleteDigitalContentAwardAsync(Guid awardId);

        public Task<Response<CelebirtyAwardDTO>> GetCelebirtyAwardByIdAsync(Guid awardId);
        public Task<Response<IEnumerable<CelebirtyAwardDTO>>> GetAllCelebirtyAwardsAsync();
        public Task<Response<IEnumerable<CelebirtyAwardDTO>>> GetAwardsByCelebrityAsync(Guid celebirtyId);
        public Task<Response<CelebirtyAwardDTO>> CreateCelebirtyAwardAsync(CreateCelebirtyAwardDTO awardDto);
        public Task<Response<bool>> AddAwardToCelebirtyAsync(Guid awardId, Guid celebirtyId, int year);
        public Task<Response<bool>> RemoveAwardFromCelebrityAsync(Guid awardId, Guid celebirtyId, int Year);
        public Task<Response<CelebirtyAwardDTO>> UpdateCelebirtyAwardAsync(UpdateCelebirtyAwardDTO awardDTO);
        public Task<Response<bool>> DeleteCelebirtyAwardAsync(Guid awardId);
    }
}
