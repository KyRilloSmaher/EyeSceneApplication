using EyeScenceApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.DigitalContent
{
    public class DigitalContentDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public Nationality CountryOfOrigin { get; set; }
        public int DurationByMinutes { get; set; }
        public int ReleaseYear { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public double Rate { get; set; } = 0;
        public DateTime UploadingDate { get; set; } = DateTime.UtcNow;
        public string Type { get; set; } = string.Empty; // e.g., Movie, Series, Documentary
        public ICollection<string> Genres { get; set; } = new List<string>();
    }
}
