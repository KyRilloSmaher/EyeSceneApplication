using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Genres
{
    public class GenreDTO
    {
        public byte Id { get; set; }
        public string Name { get; set; } 
        public string PosterUrl { get; set; }
    }
}
