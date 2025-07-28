using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using EyeScenceApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EyeScenceApp.Infrastructure.Repositories
{
    public class DigitalContentRepository :GenericRepository<DigitalContent> ,IDigitalContentRepository
    {
        private readonly ApplicationDbContext _context;

        public DigitalContentRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<bool> IsDigitalContentExist(Guid Id) => await _context.DigitalContents.AnyAsync(dc => dc.Id == Id);
        public async Task<DigitalContent?> GetDigitalContentByIdAsync(Guid Id)
        {
            return await _context.DigitalContents
                                    .Include(dc => dc.Image)
                                    .Include(dc => dc.Genres)
                                    .Include(sc=>sc.MovieCasts)
                                    .AsTracking()
                                    .FirstOrDefaultAsync(dc => dc.Id == Id);
        }
        public async Task<bool> AddToDigitalContentCrewAsync(DigitalContent digitalContent, Crew crewMember)
        {
            if (digitalContent == null || crewMember == null)
            {
                throw new ArgumentNullException("DigitalContent or CrewMember cannot be null.");
            }

            var workOn = new WorksOn
            {
                DigitalContentId = digitalContent.Id,
                CrewId = crewMember.Id
            };

            _dbContext.WorksOn.Add(workOn);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveFromDigitalContentCrewAsync(Guid digitalContentId, Guid crewMemberId)
        {
            var workOn = await _dbContext.WorksOn
                .FirstOrDefaultAsync(w => w.DigitalContentId == digitalContentId && w.CrewId == crewMemberId);
            if (workOn == null)
            {
                return false; // No such relationship exists
            }
            _dbContext.WorksOn.Remove(workOn);
            return await _dbContext.SaveChangesAsync() > 0;

        }

        public async Task<Crew?> GetCrewAsync(Guid crewId, bool AsTracked = false)
        {
            var query = _dbContext.Crews
                                      .Include(a => a.Images).ThenInclude(i => i.Image)
                                      .Include(a => a.WorksOn)
                                      .AsQueryable();

            if (!AsTracked)
                query = query.AsNoTracking();

            var result = await query.FirstOrDefaultAsync(a => a.Id == crewId);
            return result;
        }
    }
}
