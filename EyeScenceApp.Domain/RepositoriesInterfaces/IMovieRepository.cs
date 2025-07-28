using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface IMovieRepository :IGenericRepository<Movie>
    {
        public Task<IEnumerable<Movie?>> GetMoviesByTitleAsync(string title);
        public Task<IEnumerable<Movie?>> GetMoviesByGenreIdAsync(byte genreId);
        public Task<IEnumerable<Movie?>> GetNewAddedMoviesAsync(byte genreId = 0);
        public Task<IEnumerable<Movie?>> GetTopRatedMoviesAsync(int topCount = 10);
        public Task<IEnumerable<Movie?>> GetMoviesOrderedByReveuesAsync(int topCount = 10);
    }
}
