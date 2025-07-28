using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Application.DTOs.Crew.Directors;
using EyeScenceApp.Application.DTOs.DigitalContent;


namespace EyeScenceApp.Application.IServices
{
    public interface IDirectorService
    {
        public Task<Response<DirectorDTO?>> GetDirectorByIdAsync(Guid DirectorId);
        public Task<Response<ICollection<DirectorDTO>>> GetAllDirectorsAsync();
        public Task<Response<ICollection<DirectorDTO>>> FilterDirectorAsync(FilterDTO dto);
        public Task<Response<ICollection<CelebirtyAwardDTO>>> GetDirectorAwardsAsync(Guid DirectorId);
        public Task<Response<ICollection<DigitalContentDTO>>> GetDirectorWorksAsync(Guid DirectorId);
        public Task<Response<bool>> UpdateDirectorAsync(UpdateDirectorDTO DirectorDTO);
        public Task<Response<bool>> CreateDirectorAsync(CreateDirectorDTO DirectorDTO);
        public Task<Response<bool>> DeleteDirectorAsync(Guid DirectorId);
    }
}
