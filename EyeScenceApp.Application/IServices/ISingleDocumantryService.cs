

using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Application.DTOs.SingleDocumentries;
using EyeScenceApp.Domain.Enums;

namespace EyeScenceApp.Application.IServices
{
    public interface ISingleDocumantryService
    {
        public Task<Response<DocumantaryDTO>> GetSingleDocumantryAsync(Guid documantryId);
        public Task<Response<ICollection<DocumantaryDTO>>> SearchDocumantriesByTiltle(string title);
        public Task<Response<ICollection<DocumantaryDTO>>> GetAllDocumantriesAsync();
        public Task<Response<ICollection<DocumantaryDTO>>> GetAllDocumantriesByCountryAsync(Nationality countryName);
        public Task<Response<ICollection<DocumantaryDTO>>> GetAllDocumantriesByRealseyearAsync(int year);
        public Task<Response<ICollection<DocumantaryDTO>>> getTopRatedDocumantries();
        public Task<Response<ICollection<DocumantaryDTO>>> GetNewRealseDocumantries();
        public Task<Response<ICollection<ActorDTO>>> GetSingleDocumantryCastAsync(Guid DocumantryId);
        public Task<Response<DocumantaryDTO>> CreateSingleDocumantryAsync(CreateDocumantryDTO createDocumantaryDTO);
        public Task<Response<DocumantaryDTO>> UpdateSingleDocumantryAsync(UpdateDocumantryDTO updateDocumantaryDTO);
        public Task<Response<bool>> DeleteSingleDocumantryAsync(Guid documantryId);
    }
}
