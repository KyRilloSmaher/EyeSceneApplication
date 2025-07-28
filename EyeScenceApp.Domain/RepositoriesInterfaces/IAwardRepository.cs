using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface IAwardRepository : IGenericRepository<Award>
    {
        public Task<Award?> GetAwardByNameAsync(string name , bool isTracked = false);
        public Task<IEnumerable<DigitalContentAward>> GetAllDigitalContentAwardsAsync();
        public Task<IEnumerable<CelebirtyAward>> GetAllCelebirtyAwardsAsync();
        public Task<IEnumerable<Award>> GetAwardsByOrganizationAsync(string organizationName ,bool isTracked = false);
        public Task<IEnumerable<Award>> GetAwardsByCategoryAsync(string category ,bool isTracked = false);
        public Task<DigitalContentAward?> GetDigitalContentAwardByIdAsync(Guid id, bool isTracked = false);
        public Task<CelebirtyAward?> GetCelebirtyAwardByIdAsync(Guid id, bool isTracked = false);
        public Task<IEnumerable<CelebirtyAward>> GetAwardsByCelebrityAsync(Guid celebirtyId, bool isTracked = false);
        public Task<IEnumerable<DigitalContentAward>> GetAwardsByDigitalContentAsync(Guid digitalContentId, bool isTracked = false);
        public Task<DigitalContentAward> CreateDigitalContentAwardAsync(DigitalContentAward award);
        public Task<CelebirtyAward> CreateCelebirtyAwardAsync(CelebirtyAward award);
        public Task<bool> AddAwardToDigitalContentAsync(Award award, DigitalContent digitalContent, int Year);
        public Task<bool> AddAwardToCelebirtyAsync(Award award, Celebrity celebirty, int year);
        public Task<bool> RemoveAwardFromCelebrityAsync(Guid awardId, Guid celebirtyId, int Year);
        public Task<bool> RemoveAwardFromDigitalContentAsync(Guid awardId, Guid celdigitalContentIdebirtyId, int Year);

    }
}
