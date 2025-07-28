using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.EpisodeDTO
{
    public class CreateEpisodeDTO
    {
        public IFormFile Poster { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public int DurationByMinutes { get; set; }
        public int Season { get; set; }
        public int EpisodeNumber { get; set; }
        public Guid SeriesId { get; set; }
    }
}
