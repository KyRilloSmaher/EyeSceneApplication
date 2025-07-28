using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using EyeScenceApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Infrastructure.Repositories
{
    public class CastRepository : GenericRepository<Actor>,ICastRepository
    {
        
        #region Field(s)
        private readonly ApplicationDbContext _dbContext;
        #endregion

        #region Constructor(s)
        public CastRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext ?? throw new ArgumentNullException(nameof(_dbContext), "ApplicationDbContext cannot be null.");

        }


        #endregion

        #region Method(s)
   
        public async override Task<IEnumerable<Actor>> GetAllAsync(bool AsTracking = false) {
            return AsTracking
                            ? await _dbContext.Actors.Include(a=>a.Images).ThenInclude(i=>i.Image).ToListAsync()
                            : await _dbContext.Actors.Include(a => a.Images).ThenInclude(i => i.Image).AsNoTracking().ToListAsync();
        }
        public async override Task<Actor?> GetByIdAsync(Guid id, bool AsTracking = false) {
            var query = _dbContext.Actors
                .Include(a => a.Images).ThenInclude(i => i.Image)
                .Include(a => a.MovieCasts)
                .AsQueryable();

            if (!AsTracking)
                query = query.AsNoTracking();

            var result = await query.FirstOrDefaultAsync(a => a.Id == id);
            if (result.TotalMovies != result.MovieCasts.Count)
            {
                result.TotalMovies = result.MovieCasts.Count;
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }
        public async override Task<IQueryable<Actor>> FilterListAsync<TKey>(
            Expression<Func<Actor, TKey>> orderBy,
            Expression<Func<Actor, bool>> searchPredicate = null,
            bool ascending = true)
        {
            IQueryable<Actor> query = _dbContext.Actors
                .Include(a => a.Images)
                .ThenInclude(i => i.Image);

            if (searchPredicate != null)
            {
                query = query.Where(searchPredicate);
            }

            query = ascending ?
                query.OrderBy(orderBy) :
                query.OrderByDescending(orderBy);

            return await Task.FromResult(query);
        }
        public async Task<IEnumerable<Actor>> GetActorsByNameAsync(string name)
        {
            return await _dbContext.Actors
                               .Include(a => a.Awards)
                               .Include(a=>a.Images).ThenInclude(i=>i.Image)
                               .Where(a => (a.FirstName + " " + a.MiddleName + " " + a.LastName).Contains(name))
                               .ToListAsync();
        }

        public async Task<Actor?> GetActorByFullNameAsync(string fullName)
        {
            return await _dbContext.Actors
                                 .Include(a => a.Awards)
                                 .FirstOrDefaultAsync(a => (a.FirstName + " " + a.MiddleName + " " + a.LastName) == fullName);
        
        }

        public async Task<IEnumerable<Actor>> GetActorsByDocumantaryIdAsync(Guid SingleDocumataryId)
        {
                             return await _dbContext.MovieCasts
                                                     .Include(a => a.Actor)
                                                        .ThenInclude(ac => ac.Images)
                                                            .ThenInclude(i => i.Image)
                                                    .Where(w => w.DigitalContentId == SingleDocumataryId)
                                                    .Select(s => s.Actor)
                                                    .ToListAsync();
        }

        public async Task<IEnumerable<Actor>> GetActorsByMovieIdAsync(Guid movieId)
        {
            return await _dbContext.MovieCasts
                                               .Include(a => a.Actor)
                                                 .ThenInclude(ac=>ac.Images)
                                                    .ThenInclude(i=>i.Image)
                                               .Where(w => w.DigitalContentId == movieId)
                                               .Select(s => s.Actor)
                                               .ToListAsync();
        }


        public async Task<IEnumerable<Actor>> GetActorsBySerieIdAsync(Guid seriesId)
        {
            return await _dbContext.MovieCasts
                                              .Include(a => a.Actor)
                                                 .ThenInclude(ac => ac.Images)
                                                    .ThenInclude(i => i.Image)
                                             .Where(w => w.DigitalContentId == seriesId)
                                             .Select(s => s.Actor)
                                             .ToListAsync();
        }

        public async Task<IEnumerable<DigitalContent>> GetActorWorksAsync(Guid actorId)
        {
            if (!await _dbContext.Actors.AnyAsync(a => a.Id == actorId))
                return Enumerable.Empty<DigitalContent>();

            return await _dbContext.MovieCasts
                                 .Where(mc => mc.ActorId == actorId)
                                 .Include(mc => mc.DigitalContent)
                                 .Select(mc => mc.DigitalContent)
                                 .ToListAsync();
        }
        public async Task<bool> AddActorToDigitalContentAsync(DigitalContent digitalContent, Actor actor)
        {
            if (digitalContent == null) throw new ArgumentNullException(nameof(digitalContent));
            if (actor == null) throw new ArgumentNullException(nameof(actor));
            var newMovieCast = new MovieCast
            {
                DigitalContentId = digitalContent.Id,
                ActorId = actor.Id
            };
            _dbContext.MovieCasts.Add(newMovieCast);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveActorFromDigitalContentAsync(Guid digitalContentId, Guid actorId)
        {
            var movieCast = await _dbContext.MovieCasts
                                        .FirstOrDefaultAsync(mc => mc.DigitalContentId == digitalContentId && mc.ActorId == actorId);
            if (movieCast == null)
            {
                return false; // Actor not found in the specified digital content
            }
            _dbContext.MovieCasts.Remove(movieCast);
            await _dbContext.SaveChangesAsync();
            return true; // Actor successfully removed from the digital content
        }

        #endregion
    }
}
