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
    public interface IDigitalContentRepository :IGenericRepository<DigitalContent>
    {
        public  Task<bool> IsDigitalContentExist(Guid Id);
        public  Task<DigitalContent?> GetDigitalContentByIdAsync(Guid Id);
        public Task<bool> AddToDigitalContentCrewAsync(DigitalContent digitalContent, Crew crewMember);
        public Task<bool> RemoveFromDigitalContentCrewAsync(Guid digitalContentId, Guid crewMemberId);

        // i should remove that from here 
        Task<Crew?> GetCrewAsync(Guid crewId, bool AsTracked = false);
    }
}
