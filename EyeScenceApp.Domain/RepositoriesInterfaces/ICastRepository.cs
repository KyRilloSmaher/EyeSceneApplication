using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface ICastRepository : IGenericRepository<Actor>
    {
        public Task<IEnumerable<Actor>> GetActorsByNameAsync(string name);
        public Task<Actor?> GetActorByFullNameAsync(string fullName);
        public Task<IEnumerable<Actor>> GetActorsByMovieIdAsync(Guid movieId);
        public Task<IEnumerable<Actor>> GetActorsBySerieIdAsync(Guid seriesId);
        public Task<IEnumerable<Actor>> GetActorsByDocumantaryIdAsync(Guid SingleDocumataryId);
        public Task<IEnumerable<DigitalContent>> GetActorWorksAsync(Guid ActorId);
        public Task<bool> AddActorToDigitalContentAsync(DigitalContent digitalContent,Actor actor);
        public Task<bool> RemoveActorFromDigitalContentAsync(Guid digitalContentId, Guid actorId);
    }
}
