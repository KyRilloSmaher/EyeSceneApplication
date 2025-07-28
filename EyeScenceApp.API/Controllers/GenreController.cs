using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.Genres;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet(Router.Genres.GetById)]
        public async Task<IActionResult> GetById([FromRoute]byte Id)
        {
            var response = await _genreService.GetGenreByIdAsync(Id);
            return Router.FinalResponse<GenreDTO>(response);

        }

        [HttpGet(Router.Genres.GetGenreByName)]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var response = await _genreService.GetGenreByNameAsync(name);
            return Router.FinalResponse<GenreDTO>(response);

        }
        [HttpGet(Router.Genres.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _genreService.GetAllAsync();
            return Router.FinalResponse<IEnumerable<GenreDTO>>(response);

        }

        [HttpPost(Router.Genres.CreateGenre)]
        public async Task<IActionResult> CreateGenre([FromForm] CreateGenreDTO dto)
        {
            var response = await _genreService.CreateGenreAsync(dto);
            return Router.FinalResponse<bool>(response);
        }

        [HttpPut(Router.Genres.UpdateGenre)]
        public async Task<IActionResult> UpdateGenre([FromForm] UpdateGenreDTO dto)
        {
            var response = await _genreService.UpdateGenreAsync(dto);
            return Router.FinalResponse<bool>(response);
        }

        [HttpDelete(Router.Genres.DeleteGenre)]
        public async Task<IActionResult> DeleteGenre([FromRoute]byte Id)
        {
            var response = await _genreService.DeleteAsync(Id);
            return Router.FinalResponse<bool>(response);
        }
    }
}
