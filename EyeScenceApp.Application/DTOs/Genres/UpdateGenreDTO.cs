using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Genres
{
    public class UpdateGenreDTO
    {
        public byte Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IFormFile? Poster { get; set; }
    }
}
