using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.SingleDocumentries;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EyeScenceApp.API.Controllers
{
  
    [ApiController]
    public class DocumantriesController : ControllerBase
    {
        private readonly ISingleDocumantryService _singleDocumantryService;



    
        public DocumantriesController(ISingleDocumantryService singleDocumantryService)
        {
            _singleDocumantryService = singleDocumantryService;
        }

        [HttpGet(Router.Documantries.GetAll)]
        public async Task<IActionResult> GetAllDocumantriesAsync()
        {
            var response = await _singleDocumantryService.GetAllDocumantriesAsync();
            return Router.FinalResponse<ICollection<DocumantaryDTO>>(response);
        }
        [HttpGet(Router.Documantries.GetDocumantryById)]
        public async Task<IActionResult> GetSingleDocumantryAsync([FromRoute] Guid Id)
        {
            var response = await _singleDocumantryService.GetSingleDocumantryAsync(Id);
            return Router.FinalResponse<DocumantaryDTO>(response);
        }

      
        [HttpGet(Router.Documantries.SearchDocumantries)]
        public async Task<IActionResult> SearchDocumantriesAsync([FromQuery] string searchTerm)
        {
            var response = await _singleDocumantryService.SearchDocumantriesByTiltle(searchTerm);
            return Router.FinalResponse<ICollection<DocumantaryDTO>>(response);
        }
        [HttpGet(Router.Documantries.SearchDocumantriesByCountry)]
        public async Task<IActionResult> SearchDocumantriesByCountryAsync([FromQuery] Nationality country)
        { 
        
            var response = await _singleDocumantryService.GetAllDocumantriesByCountryAsync(country);
            return Router.FinalResponse<ICollection<DocumantaryDTO>>(response);
        }
        [HttpGet(Router.Documantries.GetAllDocumantriesByReleaseYear)]
        public async Task<IActionResult> SearchDocumantriesByRealseyearAsync([FromQuery] int year)
        {
            var response = await _singleDocumantryService.GetAllDocumantriesByRealseyearAsync(year);
            return Router.FinalResponse<ICollection<DocumantaryDTO>>(response);
        }
        [HttpGet(Router.Documantries.TopRatedDocumantries)]
        public async Task<IActionResult> GetTopRatedDocumantriesAsync()
        {
            var response = await _singleDocumantryService.getTopRatedDocumantries();
            return Router.FinalResponse<ICollection<DocumantaryDTO>>(response);
        }
        [HttpGet(Router.Documantries.NewReleaseDocumantries)]
        public async Task<IActionResult> GetNewRealseDocumantriesAsync()
        {
            var response = await _singleDocumantryService.GetNewRealseDocumantries();
            return Router.FinalResponse<ICollection<DocumantaryDTO>>(response);
        }

        [HttpPost(Router.Documantries.CreateDocumantry)]
        public async Task<IActionResult> CreateSingleDocumantryAsync([FromForm] CreateDocumantryDTO createDocumantaryDTO)
        {
            var response = await _singleDocumantryService.CreateSingleDocumantryAsync(createDocumantaryDTO);
            return Router.FinalResponse<DocumantaryDTO>(response);
        }

        [HttpPut(Router.Documantries.UpdateDocumantry)]
        public async Task<IActionResult> UpdateSingleDocumantryAsync([FromForm] UpdateDocumantryDTO updateDocumantaryDTO)
        {
            var response = await _singleDocumantryService.UpdateSingleDocumantryAsync(updateDocumantaryDTO);
            return Router.FinalResponse<DocumantaryDTO>(response);
        }

        [HttpDelete(Router.Documantries.DeleteDocumantry)]
        public async Task<IActionResult> DeleteSingleDocumantryAsync([FromRoute] Guid documantryId)
        {
            var response = await _singleDocumantryService.DeleteSingleDocumantryAsync(documantryId);
            return Router.FinalResponse<bool>(response);

        }

     }
}
