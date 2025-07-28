using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Application.DTOs.Movies;

using EyeScenceApp.Application.IServices;
using EyeScenceApp.Application.Services;
using EyeScenceApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet(Router.Movies.GetAll)]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var response = await _movieService.GetAllMoviesAsync();
            return Router.FinalResponse<ICollection<MovieDTO>>(response);
        }
        [HttpGet(Router.Movies.GetMovieById)]
        public async Task<IActionResult> GetSingleMovieAsync([FromRoute] Guid Id)
        {
            var response = await _movieService.GetMovieAsync(Id);
            return Router.FinalResponse<MovieDTO>(response);
        }
        [HttpGet(Router.Movies.GetMovieCast)]
        public async Task<IActionResult> SearchMoviesAsync([FromRoute]Guid Id)
        {
            var response = await _movieService.GetMovieCastAsync(Id);
            return Router.FinalResponse<ICollection<ActorDTO>>(response);
        }

        [HttpGet(Router.Movies.SearchMovies)]
        public async Task<IActionResult> SearchMoviesAsync([FromQuery] string searchTerm)
        {
            var response = await _movieService.SearchMoviesByTiltle(searchTerm);
            return Router.FinalResponse<ICollection<MovieDTO>>(response);
        }
        [HttpGet(Router.Movies.SearchMoviesByCountry)]
        public async Task<IActionResult> SearchMoviesByCountryAsync([FromQuery] Nationality country)
        {

            var response = await _movieService.GetAllMoviesByCountryAsync(country);
            return Router.FinalResponse<ICollection<MovieDTO>>(response);
        }
        [HttpGet(Router.Movies.GetAllMoviesByReleaseYear)]
        public async Task<IActionResult> SearchMoviesByRealseyearAsync([FromQuery] int year)
        {
            var response = await _movieService.GetAllMoviesByRealseyearAsync(year);
            return Router.FinalResponse<ICollection<MovieDTO>>(response);
        }
        [HttpGet(Router.Movies.TopRatedMovies)]
        public async Task<IActionResult> GetTopRatedMoviesAsync()
        {
            var response = await _movieService.GetTopRatedMoviesAsync();
            return Router.FinalResponse<ICollection<MovieDTO>>(response);
        }
        [HttpGet(Router.Movies.OrderMoviesByRevenues)]
        public async Task<IActionResult> OrderMoviesByRevenuesAsync()
        {
            var response = await _movieService.GetTopMoviesByRevenuesAsync();
            return Router.FinalResponse<ICollection<MovieDTO>>(response);
        }
        [HttpGet(Router.Movies.NewReleaseMovies)]
        public async Task<IActionResult> GetNewRealseMoviesAsync()
        {
            var response = await _movieService.GetNewRealseMoviesAsync();
            return Router.FinalResponse<ICollection<MovieDTO>>(response);
        }

        [HttpPost(Router.Movies.CreateMovie)]
        public async Task<IActionResult> CreateSingleMovieAsync([FromForm] CreateMovieDTO createMovieDTO)
        {
            var response = await _movieService.CreateMovieAsync(createMovieDTO);
            return Router.FinalResponse<MovieDTO>(response);
        }

        [HttpPut(Router.Movies.UpdateMovie)]
        public async Task<IActionResult> UpdateSingleMovieAsync([FromForm] UpdateMovieDTO updateMovieDTO)
        {
            var response = await _movieService.UpdateMovieAsync(updateMovieDTO);
            return Router.FinalResponse<MovieDTO>(response);
        }

        [HttpDelete(Router.Movies.DeleteMovie)]
        public async Task<IActionResult> DeleteSingleMovieAsync([FromRoute] Guid MovieId)
        {
            var response = await _movieService.DeleteMovieAsync(MovieId);
            return Router.FinalResponse<bool>(response);

        }
    }
}
