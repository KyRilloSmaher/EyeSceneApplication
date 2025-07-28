using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.DigitalContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface IActorService
    {
        public Task<Response<ActorDTO?>> GetActorByIdAsync(Guid actorId);
        public Task<Response<ICollection<ActorDTO>>> SearchActorAsync(string name);
        public Task<Response<ICollection<ActorDTO>>> GetAllActorsAsync();
        public Task<Response<ICollection<ActorDTO>>> FilterActorAsync(ActorFilterDTO dto);
        public Task<Response<ICollection<CelebirtyAwardDTO>>> GetActorAwardsAsync(Guid actorId);
        public Task<Response<ICollection<DigitalContentDTO>>> GetActorWorksAsync(Guid actorId);
        public Task<Response<bool>> UpdateActorAsync(UpdateActorDTO actorDto);
        public Task<Response<bool>> CreateActorAsync(CreateActorDTO actorDto);
        public Task<Response<bool>> DeleteActorAsync(Guid actorId);
    }
}
