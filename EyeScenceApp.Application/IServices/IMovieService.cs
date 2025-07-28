using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Application.DTOs.Movies;
using EyeScenceApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface IMovieService
    {
        public Task<Response<MovieDTO>> GetMovieAsync(Guid movieId);
        public Task<Response<ICollection<MovieDTO>>> SearchMoviesByTiltle(string title);
        public Task<Response<ICollection<MovieDTO>>> GetAllMoviesAsync();
        public Task<Response<ICollection<MovieDTO>>> GetAllMoviesByCountryAsync(Nationality countryName);
        public Task<Response<ICollection<MovieDTO>>> GetAllMoviesByRealseyearAsync(int year);
        public Task<Response<ICollection<MovieDTO>>> GetTopRatedMoviesAsync();
        public Task<Response<ICollection<MovieDTO>>> GetNewRealseMoviesAsync();
        public Task<Response<ICollection<MovieDTO>>> GetTopMoviesByRevenuesAsync();
        public Task<Response<ICollection<ActorDTO>>> GetMovieCastAsync(Guid movieId);
        public Task<Response<MovieDTO>> CreateMovieAsync(CreateMovieDTO createMovieDTO);
        public Task<Response<MovieDTO>> UpdateMovieAsync(UpdateMovieDTO updateDocumantaryDTO);
        public Task<Response<bool>> DeleteMovieAsync(Guid documantryId);

    }
}


