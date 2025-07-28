
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.DigitalContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface IDigitalContentService
    {
        public Task<Response<bool>> AddGenreToDigitalContentAsync(AddGenreToDigitalContentRequest request);
        public Task<Response<bool>> AddActorToDigitalContentCastAsync(AddActorToDigitalContentCastRequest request);
        public Task<Response<bool>> AddCrewMemberToDigitalContentAsync(AddCrewMemberFromDigitalContentCastRequest request);
        public Task<Response<bool>> AddAwardToDigitalContentAsync(AddAwardToDigitalContentRequest request);
        public Task<Response<bool>> DeleteGenreFromDigitalContentAsync(DeleteGenreFromDigitalContentRequest request);
        public Task<Response<bool>> DeleteActorFromDigitalContentCastAsync(DeleteActorFromDigitalContentCastRequest request);
        public Task<Response<bool>> DeleteCrewMemberFromDigitalContentAsync(DeleteCrewMemberFromDigitalContentCastRequest request);
        public Task<Response<bool>> DeleteAwardFromDigitalContentAsync(DeleteAwardFromDigitalContentRequest request);


    }
}
