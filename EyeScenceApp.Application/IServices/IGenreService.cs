using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface IGenreService
    {
        public Task<Response<GenreDTO>> GetGenreByIdAsync(byte Id);
        public Task<Response<GenreDTO>> GetGenreByNameAsync(string name);
        public Task <Response<IEnumerable<GenreDTO>>> GetAllAsync();
        public Task<Response<bool>> CreateGenreAsync(CreateGenreDTO dto);
        public Task<Response<bool>> UpdateGenreAsync(UpdateGenreDTO dto);
        public Task<Response<bool>> DeleteAsync(byte Id);
    }
}
