using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Awards
{
    public class AwardDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public DateTime AwardedDate { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Organization { get; set; } = string.Empty;
    }
}
