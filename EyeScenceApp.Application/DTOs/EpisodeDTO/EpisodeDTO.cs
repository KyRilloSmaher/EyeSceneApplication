using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.EpisodeDTO
{
    public class EpisodeDTO
    {
        public Guid Id { get; set; }
        public string PosterUrl { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public int DurationByMinutes { get; set; }
        public int Season { get; set; }
        public int EpisodeNumber { get; set; }
        public Guid SeriesId { get; set; }
    }
}
